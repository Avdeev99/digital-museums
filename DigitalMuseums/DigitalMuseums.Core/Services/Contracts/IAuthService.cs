using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO;
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
        /// <returns>Access token and user.</returns>
        Task<AuthDto> AuthenticateWithGoogle(User user);
    }
}