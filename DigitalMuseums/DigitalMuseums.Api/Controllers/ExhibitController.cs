using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Exhibit;
using DigitalMuseums.Api.Contracts.Responses.Exhibit;
using DigitalMuseums.Api.Contracts.Responses.Museum;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/exhibit")]
    public class ExhibitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExhibitService _exhibitService;

        public ExhibitController(IMapper mapper, IExhibitService exhibitService)
        {
            _mapper = mapper;
            _exhibitService = exhibitService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateExhibitRequest request)
        {
            var exhibitDto = _mapper.Map<CreateExhibitDto>(request);
            await _exhibitService.CreateAsync(exhibitDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateExhibitRequest request)
        {
            var exhibitDto = _mapper.Map<UpdateExhibitDto>(request);
            exhibitDto.Id = id;
            if (exhibitDto.ImagesData != null)
            {
                exhibitDto.ImagesData.ExhibitId = id;
            }

            await _exhibitService.UpdateAsync(exhibitDto);
            
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exhibit = await _exhibitService.GetAsync(id);
            var result = _mapper.Map<GetExhibitResponse>(exhibit);
            
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterExhibitsRequest filter)
        {
            var filterDto = _mapper.Map<FilterExhibitsDto>(filter);
            
            var filteredItems = await _exhibitService.GetFilteredAsync(filterDto);
            var result = _mapper.Map<List<GetFilteredExhibitsResponseItem>>(filteredItems);

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exhibitService.DeleteAsync(id);
            
            return Ok();
        }
    }
}