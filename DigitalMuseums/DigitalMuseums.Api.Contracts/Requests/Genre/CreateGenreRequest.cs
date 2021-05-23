using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Genre
{
    public class CreateGenreRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}