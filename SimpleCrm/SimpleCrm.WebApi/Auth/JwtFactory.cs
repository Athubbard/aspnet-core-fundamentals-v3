using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SimpleCrm.WebApi.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, userName),
                  new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()), // See video for implementation of JtiGenerator
                  new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),  // See video for implementation of ToUnixEpochDate
                    ClaimValueTypes.Integer64),
                  identity.FindFirst(Constants.JwtClaimIdentifiers.Rol),
                  identity.FindFirst(Constants.JwtClaimIdentifiers.Id)
                };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
              issuer: _jwtOptions.Issuer,
              audience: _jwtOptions.Audience,
              claims: claims,
              notBefore: _jwtOptions.NotBefore,
              expires: _jwtOptions.Expiration,
              signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                      new Claim(Constants.JwtClaimIdentifiers.Id, id),
                      new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess)
                  });
        }
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options) 
        {
            if (options == null)
            {
                new ArgumentNullException(nameof(options));
            }
            
            if (options.ValidFor <= TimeSpan.Zero)
            {
                new ArgumentOutOfRangeException(nameof(options));
            }

            if (options.SigningCredentials == null)
            {
                new ArgumentNullException(nameof(options));
            }
            if (options.JtiGenerator == null)
            {
                new ArgumentNullException(nameof(options));
            }

        }

        
    }
}





