using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Core.Services.Contracts
{
    /// <summary>
    /// Service for user authentication.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates the specified user with google.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Access token.</returns>
        string AuthenticateWithGoogle(User user);
    }
}