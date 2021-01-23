using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests
{
    /// <summary>
    /// Represents authentication request model.
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Gets or user email.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user password.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
