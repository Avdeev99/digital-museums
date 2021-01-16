using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests
{
    /// <summary>
    /// Represents google authentication api model.
    /// </summary>

    public class GoogleAuthRequest
    {
        /// <summary>
        /// Gets or sets google authentication id token.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string IdToken { get; set; }
    }
}