namespace DigitalMuseums.Core.Domain.DTO.Cart
{
    public class UpdateCartItemDto
    {
        public int SouvenirId { get; set; }

        public int Quantity { get; set; }
        
        public int UserId { get; set; }
    }
}