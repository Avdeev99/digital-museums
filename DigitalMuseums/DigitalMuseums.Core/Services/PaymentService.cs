using System.Collections.Generic;
using System.Linq;
using DigitalMuseums.Core.Constants;
using DigitalMuseums.Core.Domain.DTO.Payment;
using DigitalMuseums.Core.Services.Contracts;
using Stripe;
using Stripe.Checkout;

namespace DigitalMuseums.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ChargeService _chargeService;

        public PaymentService(ChargeService chargeService)
        {
            _chargeService = chargeService;
        }

        public Charge ProcessStripeCharge(ProcessStripeChargeDto request)
        {
            var options = CreateSessionOptions(request);
            var charge = _chargeService.Create(options);

            return charge;
        }

        private static ChargeCreateOptions CreateSessionOptions(ProcessStripeChargeDto request)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long) (request.CurrentCart.OrderDetails.Sum(x => x.Price) * 100),
                Currency = ServiceConstants.HryvniaCurrencyCode,
                Source = request.Token
            };
            
            return options;
        }
    }
}