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
        public IActionResult Create(AddGenreRequest request)
        {
            var genre = mapper.Map<Genre>(request);
            _genreService.Create(genre);
            
            return Ok();
        }
    }
}