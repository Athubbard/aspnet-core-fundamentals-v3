using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SimpleCrm.SqlDbServices;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace SimpleCrm.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string SecretKey = "sdkdhsHOQPdjspQNSHsjsSDQWJqzkpdnf"; //<-- NEW: make up a random key here
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
                // optionally: allow configuration overide of ValidFor (defaults to 120 mins)
            });
            services.AddDbContext<SimpleCrmDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SimpleCrmConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContext<CrmIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SimpleCrmConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRazorPages();

            var identityBuilder = services.AddIdentityCore<CrmUser>(o =>
            {
                //TODO: you may override any default password rules here.
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType,
              typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<CrmIdentityDbContext>();
            identityBuilder.AddRoleValidator<RoleValidator<IdentityRole>>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<CrmUser>>();
            identityBuilder.AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddScoped<ICustomerData, SqlCustomerData>();
            services.AddResponseCaching();
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                var settings = options.JsonSerializerOptions;
                settings.PropertyNameCaseInsensitive = true;
                settings.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddControllersWithViews(o =>
            {
                o.Filters.Add<GlobalExceptionFilter>();
            });

            var googleOptions = Configuration.GetSection(nameof(GoogleAuthSettings));
            services.Configure<GoogleAuthSettings>(options =>
            {
                options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
                options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
            });

            var microsoftOptions = Configuration.GetSection(nameof(MicrosoftAuthSettings));
            services.Configure<MicrosoftAuthSettings>(options =>
            {
                options.ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                options.ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientSecret)];
            });

            var tokenValidationPrms = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddGoogle(options =>  // <-- NEW
                {
                    options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
                    options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                    options.ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                })
                .AddJwtBearer(configureOptions =>
                 {
                     // ... TODO: set token options
                     configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                     configureOptions.TokenValidationParameters = tokenValidationPrms;
                     configureOptions.SaveToken = true; // allows token access in controller
                 });
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(
                  Constants.JwtClaimIdentifiers.Rol,
                  Constants.JwtClaims.ApiAccess
                ));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 48; // 1 day
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });



            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.UseWhen(
         context => !context.Request.Path.StartsWithSegments("/api"),
         appBuilder => appBuilder.UseSpa(spa =>
         {
             if (env.IsDevelopment())
             {
                 spa.Options.SourcePath = "../simple-crm-cli";
                 spa.Options.StartupTimeout = new TimeSpan(0, 0, 300); //300 seconds
                 spa.UseAngularCliServer(npmScript: "start");
             }
         }));
        }
    }
}
