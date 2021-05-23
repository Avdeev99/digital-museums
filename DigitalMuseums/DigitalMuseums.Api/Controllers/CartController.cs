using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Cart;
using DigitalMuseums.Api.Contracts.Responses.Cart;
using DigitalMuseums.Core.Domain.DTO.Cart;
using DigitalMuseums.Core.Domain.DTO.Payment;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly ILoggedInPersonProvider _loggedInPersonProvider;
        private readonly IMapper _mapper;

        public CartController(
            ICartService cartService, 
            IPaymentService paymentService,
            IMapper mapper,
            ILoggedInPersonProvider loggedInPersonProvider)
        {
            _cartService = cartService;
            _paymentService = paymentService;
            _mapper = mapper;
            _loggedInPersonProvider = loggedInPersonProvider;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemRequest request)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var addItemDto = BuildAddItemDto(request, userId);
            
            await _cartService.UpdateCartItemAsync(addItemDto);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCartItem(AddCartItemRequest request)
        {
            var userId = _loggedInPersonProvider.GetUserId();

            await _cartService.AddCartItemAsync(userId, request.SouvenirId);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentCart()
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var currentCart = await _cartService.GetCurrentCartAsync(userId);
            
            var result = _mapper.Map<GetCurrentCartResponse>(currentCart);
            return Ok(result);
        }
        
        [HttpPost("payment")]
        [Authorize]
        public async Task<IActionResult> Pay([FromBody] PayCartRequest request)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var currentCart = await _cartService.GetCurrentCartAsync(userId);
            var processStripeChargeDto = new ProcessStripeChargeDto
            {
                CurrentCart = currentCart,
                Token = request.Token
            };

            var charge = _paymentService.ProcessStripeCharge(processStripeChargeDto);
            await _cartService.ProcessCartAsync(userId);

            return Ok(charge);
        }

        [Authorize]
        [HttpDelete("souvenir/{souvenirId}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int souvenirId)
        {
            var userId = _loggedInPersonProvider.GetUserId();

            await _cartService.DeleteCartItemAsync(userId, souvenirId);

            return Ok();
        }

        private static UpdateCartItemDto BuildAddItemDto(UpdateCartItemRequest request, int userId)
        {
            var result = new UpdateCartItemDto
            {
                SouvenirId = request.SouvenirId,
                Quantity = request.Quantity,
                UserId = userId
            };

            return result;
        }
    }
}