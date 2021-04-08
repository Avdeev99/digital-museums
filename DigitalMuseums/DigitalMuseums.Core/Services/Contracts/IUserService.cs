using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.Models;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<List<BasePredefinedEntity>> GetBaseListAsync();
    }
}
