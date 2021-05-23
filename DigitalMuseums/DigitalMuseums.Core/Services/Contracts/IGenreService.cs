using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IGenreService
    {
        Task CreateAsync(Genre genre);

        Task UpdateAsync(Genre genre);

        Task DeleteAsync(int id);
    }
}