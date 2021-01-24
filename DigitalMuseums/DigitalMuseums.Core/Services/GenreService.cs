using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Genre> _genreRepository;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _genreRepository = unitOfWork.GetRepository<Genre>();
        }
        
        public void Create(Genre genre)
        {
            _genreRepository.Create(genre);
            _unitOfWork.SaveChanges();
        }
    }
}