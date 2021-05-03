using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Cart
{
    public class PayCartRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; }
    }
}