using DigitalMuseums.Core.Domain.DTO.Payment;
using Stripe;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IPaymentService
    {
        Charge ProcessStripeCharge(ProcessStripeChargeDto request);
    }
}