using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.DTO.Statistics;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IStatisticsService
    {
        Task<StatisticsDetails> Get();
    }
}