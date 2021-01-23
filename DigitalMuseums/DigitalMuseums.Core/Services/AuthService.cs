﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoHelper;
using DigitalMuseums.Auth.Tokens.Interfaces;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    /// <summary>
    /// The user authentication service.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenProvider _tokenProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> reference.</param>
        /// <param name="tokenProvider">The <see cref="ITokenProvider"/> reference.</param>
        public AuthService(IUnitOfWork unitOfWork, ITokenProvider tokenProvider)
        {
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _userRepository = unitOfWork.GetRepository<User>();
        }

        /// <inheritdoc/>
        public async Task<AuthDto> AuthenticateWithGoogleAsync(User user)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Email.Equals(user.Email));

            if (existingUser == null)
            {
                _userRepository.Create(user);
                await _unitOfWork.SaveChangesAsync();
            }

            var includes = new List<Expression<Func<User, object>>>()
            {
                u => u.Role
            };
            existingUser = await _userRepository.GetAsync(u => u.Email.Equals(user.Email), includes);

            var authResult = new AuthDto
            {
                Token = _tokenProvider.GenerateTokenForUser(existingUser),
                User = existingUser
            };
            return authResult;
        }

        /// <inheritdoc/>
        public async Task<AuthDto> AuthenticateAsync(string email, string password)
        {
            var includes = new List<Expression<Func<User, object>>>()
            {
                u => u.Role
            };
            var existingUser = await _userRepository.GetAsync(u => u.Email.Equals(email), includes);
            
            if (existingUser == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.InvalidCredentialsCode);
            }

            var isPasswordCorrect = Crypto.VerifyHashedPassword(existingUser.Password, password);
            if (!isPasswordCorrect)
            {
                throw new BusinessLogicException(BusinessErrorCodes.InvalidCredentialsCode);
            }
            
            var authResult = new AuthDto
            {
                Token = _tokenProvider.GenerateTokenForUser(existingUser),
                User = existingUser
            };
            return authResult;
        }
    }
}