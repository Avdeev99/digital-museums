using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Exhibit;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IExhibitService
    {
        Task CreateAsync(CreateExhibitDto createMuseumDto);
        
        Task UpdateAsync(UpdateExhibitDto updateExhibitDto);
        
        Task<ExhibitItem> GetAsync(int id);
        
        Task<List<FilteredExhibitItem>> GetFilteredAsync(FilterExhibitsDto filter);
        
        Task DeleteAsync(int id);
    }
}