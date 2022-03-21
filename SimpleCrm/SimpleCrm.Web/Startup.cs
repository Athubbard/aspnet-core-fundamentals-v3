using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IGreeter, ConfigurationGreeter>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
              
                app.UseExceptionHandler(new ExceptionHandlerOptions
               {
                   ExceptionHandler = context => context.Response.WriteAsync("There was something wrong here!")
                }); 
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default", "{controller=Home}/{action=Index}/{id?}");




                endpoints.MapControllerRoute(
                        "default", "{controller=About}/{action=About}/{id?}");


                endpoints.MapControllerRoute(
                        "default", "{controller=About}/{action=Phone}/{id?}");

                endpoints.MapControllerRoute(
                        "default", "{controller=About}/{action=Address}/{id?}");
            });

          

            app.Run(ctx => ctx.Response.WriteAsync("Not Found"));

        }
    }
}
