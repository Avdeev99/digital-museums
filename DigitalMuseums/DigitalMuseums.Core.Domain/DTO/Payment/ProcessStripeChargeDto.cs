using DigitalMuseums.Core.Domain.DTO.Cart;

namespace DigitalMuseums.Core.Domain.DTO.Payment
{
    public class ProcessStripeChargeDto
    {
        public string Token { get; set; }

        public CurrentCart CurrentCart { get; set; }
    }
}