using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Cart
{
    public class UpdateCartItemRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SouvenirId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}