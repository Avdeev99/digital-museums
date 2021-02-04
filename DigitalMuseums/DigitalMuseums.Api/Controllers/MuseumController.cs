using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Museum;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/museum")]
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

        [HttpPost("user")]
        public async Task<IActionResult> LinkUserToMuseum(LinkUserToMuseumRequest request)
        {
            var dto = _mapper.Map<LinkUserToMuseumDto>(request);
            await _museumService.LinkUserAsync(dto);
            
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var museum = await _museumService.GetAsync(id);
            
            return Ok(museum);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterMuseumsViewModel filter)
        {
            var filterDto = _mapper.Map<FilterMuseumsDto>(filter);
            var result = await _museumService.GetFilteredAsync(filterDto);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMuseumRequest request)
        {
            var museumDto = _mapper.Map<UpdateMuseumDto>(request);
            await _museumService.UpdateAsync(museumDto);
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _museumService.DeleteAsync(id);
            
            return Ok();
        }
    }
}