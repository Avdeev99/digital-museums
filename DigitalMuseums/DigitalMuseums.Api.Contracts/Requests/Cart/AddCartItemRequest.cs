using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Cart
{
    public class AddCartItemRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SouvenirId { get; set; }
    }
}
