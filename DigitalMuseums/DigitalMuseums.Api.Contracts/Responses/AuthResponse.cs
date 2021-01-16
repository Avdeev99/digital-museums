using DigitalMuseums.Api.Contracts.ViewModels;

namespace DigitalMuseums.Api.Contracts.Responses
{
    /// <summary>
    /// The authentication response model.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Gets or sets jwt token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public UserViewModel User { get; set; }
    }
}