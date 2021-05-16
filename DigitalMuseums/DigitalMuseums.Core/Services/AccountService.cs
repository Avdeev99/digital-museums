using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoHelper;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO.Account;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;

namespace DigitalMuseums.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILoggedInPersonProvider _loggedInPersonProvider;


        public AccountService(IUnitOfWork unitOfWork, ILoggedInPersonProvider loggedInPersonProvider)
        {
            _unitOfWork = unitOfWork;
            _loggedInPersonProvider = loggedInPersonProvider;
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var user = await _userRepository.GetAsync(u => u.Id == userId, TrackingState.Enabled);
            if (user == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserNotFoundCode);
            }

            var isPasswordValid = Crypto.VerifyHashedPassword(user.Password, changePasswordDto.OldPassword);
            if (!isPasswordValid)
            {
                throw new BusinessLogicException(BusinessErrorCodes.InvalidPassword);
            }

            var password = Crypto.HashPassword(changePasswordDto.NewPassword);
            user.Password = password;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditUserInfoAsync(string name)
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var user = await _userRepository.GetAsync(u => u.Id == userId, TrackingState.Enabled);
            if (user == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserNotFoundCode);
            }

            user.UserName = name;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> GetCurrentUser()
        {
            var userId = _loggedInPersonProvider.GetUserId();
            var includes = new List<Expression<Func<User, object>>>()
            {
                u => u.Role
            };

            var user = await _userRepository.GetAsync(u => u.Id == userId, includes);
            if (user == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.UserNotFoundCode);
            }

            return user;
        }
    }
}
