using System.Collections.Generic;
using DigitalMuseums.Core.Domain.DTO.Cart;
using DigitalMuseums.Core.Domain.DTO.Payment;
using DigitalMuseums.Core.Services;
using Moq;
using NUnit.Framework;
using Stripe;

namespace DigitalMuseums.Core.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private const string Token = nameof(Token);

        private Mock<ChargeService> _chargeServiceMock;

        private PaymentService _sut;

        [SetUp]
        public void Setup()
        {
            _chargeServiceMock = new Mock<ChargeService>();

            _sut = new PaymentService(_chargeServiceMock.Object);
        }

        [Test]
        public void ProcessStripeCharge_ChargeServiceWithRightParamsCalled()
        {
            var processStripeChargeDto = new ProcessStripeChargeDto
            {
                Token = Token,
                CurrentCart = new CurrentCart
                {
                    OrderDetails = new List<CurrentCartDetail>
                    {
                        new() {Price = 15},
                        new() {Price = 20},
                        new() {Price = 30},
                    }
                }
            };

            _sut.ProcessStripeCharge(processStripeChargeDto);
            
            _chargeServiceMock.Verify(x => x.Create(
                It.Is<ChargeCreateOptions>(cco => cco.Amount == 6500 && cco.Currency == "uah" && cco.Source == Token),
                It.IsAny<RequestOptions>()), Times.Once);
        }
    }
}