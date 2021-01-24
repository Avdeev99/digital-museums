using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Genre
{
    public class AddGenreRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}