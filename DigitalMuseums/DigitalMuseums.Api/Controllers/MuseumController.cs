using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Museum;
using DigitalMuseums.Api.Contracts.Responses.Museum;
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
            var result = _mapper.Map<GetMuseumResponse>(museum);
            
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterMuseumsRequest filter)
        {
            var filterDto = _mapper.Map<FilterMuseumsDto>(filter);
            
            var filteredItems = await _museumService.GetFilteredAsync(filterDto);
            var result = _mapper.Map<List<GetFilteredMuseumsResponseItem>>(filteredItems);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateMuseumRequest request)
        {
            var museumDto = _mapper.Map<UpdateMuseumDto>(request);
            museumDto.Id = id;
            if (museumDto.ImagesData != null)
            {
                museumDto.ImagesData.MuseumId = id;
            }

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