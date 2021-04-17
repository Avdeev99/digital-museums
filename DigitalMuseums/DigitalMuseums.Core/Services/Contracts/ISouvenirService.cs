using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Souvenir;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface ISouvenirService
    {
        Task CreateAsync(CreateSouvenirDto createSouvenirDto);
        
        Task UpdateAsync(UpdateSouvenirDto updateSouvenirDto);
        
        Task<SouvenirItem> GetAsync(int id);
        
        Task<List<FilteredSouvenirItem>> GetFilteredAsync(FilterSouvenirsDto filter);
        
        Task DeleteAsync(int id);
    }
}