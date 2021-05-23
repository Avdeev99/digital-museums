using System.Linq;
using System.Threading.Tasks;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Core.Errors;
using DigitalMuseums.Core.Exceptions;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Genre> _genreRepository;
        private readonly IBaseRepository<Museum> _museumRepository;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _genreRepository = unitOfWork.GetRepository<Genre>();
            _museumRepository = unitOfWork.GetRepository<Museum>();
        }
        
        public async Task CreateAsync(Genre genre)
        {
            await CheckSameNameExistenceAsync(genre.Name, genre.Id);
            _genreRepository.Create(genre);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            var existingGenre = await _genreRepository.GetAsync(x => x.Id == genre.Id, TrackingState.Enabled);
            if (existingGenre == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.GenreNotFoundCode);
            }

            await CheckSameNameExistenceAsync(genre.Name, genre.Id);

            existingGenre.Name = genre.Name;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _genreRepository.GetAsync(x => x.Id == id);
            if (genre == null)
            {
                throw new BusinessLogicException(BusinessErrorCodes.GenreNotFoundCode);
            }

            var museums = await _museumRepository.GetAllAsync(m => m.GenreId == id);
            if (museums.Any())
            {
                throw new BusinessLogicException(BusinessErrorCodes.GenreUsedByMuseumCode);
            }

            _genreRepository.Delete(genre);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task CheckSameNameExistenceAsync(string name, int id)
        {
            var isExisted = await _genreRepository.IsExistAsync(x =>
                    x.Name == name
                    && x.Id != id);

            if (isExisted)
            {
                throw new BusinessLogicException(BusinessErrorCodes.GenreWithSameNameExistCode);
            }
        }
    }
}