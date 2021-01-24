using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Museum;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/museum")]
    public class MuseumController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMuseumService _museumService;

        public MuseumController(IMapper mapper, IMuseumService museumService)
        {
            _mapper = mapper;
            _museumService = museumService;
        }
        
        [HttpPost]
        public IActionResult Create([FromForm] AddMuseumRequest request)
        {
            var museumDto = _mapper.Map<MuseumDto>(request);
            _museumService.Create(museumDto);

            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return BadRequest();
        }
        
        [HttpGet]
        public IActionResult GetAll() // filter
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