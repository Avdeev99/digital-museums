using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests
{
    public class SearchRequest
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string SearchTerm { get; set; }
    }
}