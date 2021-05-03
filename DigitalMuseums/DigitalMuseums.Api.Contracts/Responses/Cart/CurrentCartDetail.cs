namespace DigitalMuseums.Api.Contracts.Responses.Cart
{
    public class CurrentCartDetail
    {
        public int SouvenirId { get; set; }
        
        public SouvenirItem Souvenir { get; set; }
        
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}