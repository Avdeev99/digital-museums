using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Museum;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IMuseumService
    {
        void Create(CreateMuseumDto createMuseumDto);
        
        Task UpdateAsync(UpdateMuseumDto museumDto);

        Task DeleteAsync(int id);

        Task LinkUserAsync(LinkUserToMuseumDto linkUserToMuseumDto);
        
        Task<MuseumItem> GetAsync(int id);
        
        Task<List<FilteredMuseumItem>> GetFilteredAsync(FilterMuseumsDto filter);
    }
}