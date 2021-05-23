using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Genre;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers.Predefined
{
    [ApiController]
    [Route("/api/genre")]
    public class GenreController : GenericPredefinedEntityController<Genre, BasePredefinedEntityResponse>
    {
        private readonly IGenreService _genreService;

        public GenreController(
            IBasePredefinedEntityService<Genre> entityService,
            IMapper mapper,
            IGenreService genreService) : base(entityService, mapper)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreRequest request)
        {
            var genre = mapper.Map<Genre>(request);
            await _genreService.CreateAsync(genre);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateGenreRequest request)
        {
            var genre = mapper.Map<Genre>(request);
            await _genreService.UpdateAsync(genre);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);

            return Ok();
        }
    }
}