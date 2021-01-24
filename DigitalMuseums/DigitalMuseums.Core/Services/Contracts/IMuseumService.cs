using DigitalMuseums.Core.Domain.DTO;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IMuseumService
    {
        void Create(MuseumDto museumDto);
    }
}