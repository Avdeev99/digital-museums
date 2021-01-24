using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IMuseumService
    {
        void Create(MuseumDto museumDto);

        Task LinkUserAsync(LinkUserToMuseumDto linkUserToMuseumDto);
        
        Task<List<FilteredMuseumItem>> GetFilteredAsync(FilterMuseumsDto filter);
    }
}