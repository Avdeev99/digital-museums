using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models.Auth;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> reference.</param>
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<User>();
        }

        /// <inheritdoc/>
        public string AuthenticateWithGoogle(User user)
        {
            var users = _userRepository.GetAllAsync();
            return "ok";
        }
    }
}