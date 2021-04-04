using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task<List<BasePredefinedEntity>> GetBaseListAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var result = _mapper.Map<List<BasePredefinedEntity>>(users);

            return result;
        }
    }
}
