using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Account;
using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IAccountService
    {
        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task EditUserInfoAsync(string name);

        Task<User> GetCurrentUser();
    }
}
