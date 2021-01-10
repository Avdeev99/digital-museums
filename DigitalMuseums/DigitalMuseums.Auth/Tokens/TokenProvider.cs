using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using DigitalMuseums.Auth.Factories.Interfaces;
using DigitalMuseums.Auth.Tokens.Interfaces;
using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Auth.Tokens
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ITokenFactory _tokenFactory;

        public TokenProvider(ITokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }

        public string GenerateTokenForUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString(CultureInfo.InvariantCulture))
            };

            if (user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            }

            return _tokenFactory.Create(claims);
        } 
    }
}