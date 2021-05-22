using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Statistics;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Order;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Exhibit> _exhibitRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _exhibitRepository = unitOfWork.GetRepository<Exhibit>();
            _orderRepository = unitOfWork.GetRepository<Order>();
        }
        
        public async Task<StatisticsDetails> Get()
        {
            var usersCount = await _userRepository.CountAsync(GetEmptyFilter<User>());
            var exhibitsCount = await _exhibitRepository.CountAsync(GetEmptyFilter<Exhibit>());
            var ordersCount = await _orderRepository.CountAsync(GetEmptyFilter<Order>());
            var result = BuildStatisticsDetails(usersCount, exhibitsCount, ordersCount);

            return result;
        }

        private static Expression<Func<TEntity, bool>> GetEmptyFilter<TEntity>()
        {
            return x => true;
        }

        private static StatisticsDetails BuildStatisticsDetails(int usersCount, int exhibitsCount, int ordersCount)
        {
            return new()
            {
                UsersCount = usersCount,
                ExhibitsCount = exhibitsCount,
                OrdersCount = ordersCount
            };
        }
    }
}