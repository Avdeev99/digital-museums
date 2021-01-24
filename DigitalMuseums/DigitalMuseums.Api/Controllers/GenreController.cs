using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Genre;
using DigitalMuseums.Core.Domain.Models.Secondary;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/genre")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult Create(AddGenreRequest request)
        {
            var genre = _mapper.Map<Genre>(request);
            _genreService.Create(genre);
            
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
        
        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}