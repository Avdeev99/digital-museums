using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Museum
{
    public class LinkUserToMuseumRequest
    {
        [Required(AllowEmptyStrings = false)]
        public int MuseumId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public int UserId { get; set; }
    }
}