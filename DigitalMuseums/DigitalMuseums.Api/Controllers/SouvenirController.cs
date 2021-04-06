using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Souvenir;
using DigitalMuseums.Api.Contracts.Responses.Souvenir;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/souvenir")]
    public class SouvenirController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISouvenirService _souvenirService;

        public SouvenirController(IMapper mapper, ISouvenirService souvenirService)
        {
            _mapper = mapper;
            _souvenirService = souvenirService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSouvenirRequest request)
        {
            var createSouvenirDto = _mapper.Map<CreateSouvenirDto>(request);
            await _souvenirService.CreateAsync(createSouvenirDto);
            
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var souvenir = await _souvenirService.GetAsync(id);
            var result = _mapper.Map<GetSouvenirResponse>(souvenir);
            
            return Ok(result);
        }
        
        [HttpGet]
        public IActionResult GetAll() // filter
        {
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSouvenirRequest request)
        {
            var souvenirDto = _mapper.Map<UpdateSouvenirDto>(request);
            souvenirDto.Id = id;
            if (souvenirDto.ImagesData != null)
            {
                souvenirDto.ImagesData.SouvenirId = id;
            }

            await _souvenirService.UpdateAsync(souvenirDto);
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _souvenirService.DeleteAsync(id);
            
            return Ok();
        }
    }
}