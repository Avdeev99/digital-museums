using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Auth.Tokens.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateTokenForUser(User user);
    }
}