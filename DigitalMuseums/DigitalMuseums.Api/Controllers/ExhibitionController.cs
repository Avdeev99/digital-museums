using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Exhibition;
using DigitalMuseums.Api.Contracts.Responses.Exhibition;
using DigitalMuseums.Api.Contracts.Responses.Museum;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/exhibition")]
    public class ExhibitionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExhibitionService _exhibitionService;

        public ExhibitionController(IMapper mapper, IExhibitionService exhibitionService)
        {
            _mapper = mapper;
            _exhibitionService = exhibitionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateExhibitionRequest createRequest)
        {
            var exhibitDto = _mapper.Map<CreateExhibitionDto>(createRequest);
            await _exhibitionService.CreateAsync(exhibitDto);

            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exhibition = await _exhibitionService.GetAsync(id);
            var result = _mapper.Map<GetExhibitionResponse>(exhibition);
            
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterExhibitionsRequest filter)
        {
            var filterDto = _mapper.Map<FilterExhibitionsDto>(filter);
            
            var filteredItems = await _exhibitionService.GetFilteredAsync(filterDto);
            var result = _mapper.Map<List<GetFilteredExhibitionsResponseItem>>(filteredItems);

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateExhibitionRequest request)
        {
            var exhibitionDto = _mapper.Map<UpdateExhibitionDto>(request);
            // if (exhibitDto.ImagesData != null)
            // {
            //     exhibitDto.ImagesData.ExhibitionId = id;
            // }

            await _exhibitionService.UpdateAsync(exhibitionDto);
            
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}