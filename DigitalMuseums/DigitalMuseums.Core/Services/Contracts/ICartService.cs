using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Cart;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface ICartService
    {
        Task UpdateCartItemAsync(UpdateCartItemDto request);

        Task<CurrentCart> GetCurrentCartAsync(int userId);
        
        Task ProcessCartAsync(int userId);

        Task AddCartItemAsync(int userId, int souvenirId);

        Task DeleteCartItemAsync(int userId, int souvenirId);
    }
}