using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Cart;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Order;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        private readonly IBaseRepository<Souvenir> _souvenirRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<SouvenirOrderDetail> _orderDetailsRepository;

        public CartService(
            IUnitOfWork unitOfWork,
            IClock clock,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _clock = clock;
            _mapper = mapper;
            
            _souvenirRepository = unitOfWork.GetRepository<Souvenir>();
            _orderRepository = unitOfWork.GetRepository<Order>();
            _orderDetailsRepository = unitOfWork.GetRepository<SouvenirOrderDetail>();
        }
        
        public async Task UpdateCartItemAsync(UpdateCartItemDto request)
        {
            var souvenir = await _souvenirRepository.GetAsync(x => x.Id == request.SouvenirId);
            if (souvenir == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.SouvenirNotFoundCode);
            }

            if (souvenir.AvailableUnits < request.Quantity)
            {
                throw new BusinessLogicException(
                    BusinessErrorCodes.ExceededAvailableSouvenirCountCode, StatusCodes.Status400BadRequest);
            }

            var order = await _orderRepository.GetAsync(
                x => x.UserId == request.UserId && x.Status == OrderStatus.New);
            if (order == null)
            {
                order = await CreateOrder(request);
            }

            var orderDetail =
                await _orderDetailsRepository
                    .GetAsync(x => x.OrderId == order.Id && x.SouvenirId == request.SouvenirId, TrackingState.Enabled);
            if (orderDetail == null)
            {
                await CreateOrderDetail(request, order, souvenir);
                return;
            }

            orderDetail.Quantity = request.Quantity;
            orderDetail.Price = souvenir.Price * request.Quantity;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CurrentCart> GetCurrentCartAsync(int userId)
        {
            var includes = new List<Expression<Func<Order, object>>>
            {
                x => x.OrderDetails.Select(y => y.Souvenir.Images)
            };
            var order = await _orderRepository.GetAsync(
                x => x.UserId == userId && x.Status == OrderStatus.New,
                includes);
            if (order == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.OrderNotFoundCode, StatusCodes.Status404NotFound);
            }

            var result = _mapper.Map<CurrentCart>(order);
            return result;
        }

        public async Task ProcessCart(int userId)
        {
            var includes = new List<Expression<Func<Order, object>>>
            {
                x => x.OrderDetails.Select(y => y.Souvenir)
            };
            var order = await _orderRepository.GetAsync(
                x => x.UserId == userId && x.Status == OrderStatus.New,
                includes,
                TrackingState.Enabled);
            if (order == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.OrderNotFoundCode, StatusCodes.Status404NotFound);
            }

            order.Status = OrderStatus.Paid;
            
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Souvenir.AvailableUnits -= orderDetail.Quantity;
            }
            
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task CreateOrderDetail(UpdateCartItemDto request, Order order, Souvenir souvenir)
        {
            var newOrderDetail = new SouvenirOrderDetail
            {
                SouvenirId = request.SouvenirId,
                OrderId = order.Id,
                Quantity = request.Quantity,
                Price = souvenir.Price * request.Quantity
            };
            _orderDetailsRepository.Create(newOrderDetail);
            
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<Order> CreateOrder(UpdateCartItemDto request)
        {
            var newOrder = new Order
            {
                UserId = request.UserId,
                Status = OrderStatus.New,
                Created = _clock.GetUtcNow()
            };
            
            var order = _orderRepository.Create(newOrder);
            await _unitOfWork.SaveChangesAsync();
            
            return order;
        }
    }
}