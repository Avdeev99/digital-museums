using DigitalMuseums.Core.Domain.DTO.Souvenir;

namespace DigitalMuseums.Core.Domain.DTO.Cart
{
    public class CurrentCartDetail
    {
        public int SouvenirId { get; set; }
        
        public SouvenirItem Souvenir { get; set; }
        
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}