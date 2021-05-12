using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Exhibition;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IExhibitionService
    {
        Task CreateAsync(CreateExhibitionDto createMuseumDto);
        
        Task UpdateAsync(UpdateExhibitionDto updateExhibitDto);
        
        Task<ExhibitionItem> GetAsync(int id);
        
        Task<List<FilteredExhibitionItem>> GetFilteredAsync(FilterExhibitionsDto filter);
        
        Task DeleteAsync(int id);
    }
}