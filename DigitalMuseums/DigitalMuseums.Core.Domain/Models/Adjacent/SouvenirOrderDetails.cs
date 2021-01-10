using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Adjacent
{
    public class SouvenirOrderDetails
    {
        public int SouvenirId { get; set; }
        
        public Souvenir Souvenir { get; set; }

        public int OrderId { get; set; }

        public Order.Order Order { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}