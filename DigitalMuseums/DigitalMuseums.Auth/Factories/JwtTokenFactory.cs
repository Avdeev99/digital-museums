using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DigitalMuseums.Auth.Factories.Interfaces;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DigitalMuseums.Auth.Factories
{
    public class JwtTokenFactory : ITokenFactory
    {
        private const string IssuerClaimType = "iss";
        private const string ExpirationClaimType = "exp";

        private readonly ApiAuthOptions _settings;
        private readonly SigningCredentials _credentials;

        public JwtTokenFactory(IOptions<ApiAuthOptions> settings)
        {
            _settings = settings.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public string Create(List<Claim> claims)
        {
            if (claims.All(claim => claim.Type != IssuerClaimType))
            {
                claims.Add(new Claim(IssuerClaimType, _settings.Issuer));
            }

            if (claims.All(claim => claim.Type != ExpirationClaimType))
            {
                var expirationTime = DateTimeOffset.Now.ToUnixTimeSeconds() + _settings.ExpirationTimeInSeconds;
                claims.Add(new Claim(ExpirationClaimType, expirationTime.ToString()));
            }

            var header = new JwtHeader(_credentials);
            var payload = new JwtPayload(claims);

            var securityToken = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(securityToken);
        }
    }
}