using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Responses.Statistics;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMapper _mapper;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper)
        {
            _statisticsService = statisticsService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var statisticsDetails = await _statisticsService.Get();
            var response = _mapper.Map<GetStatisticsResponse>(statisticsDetails);

            return Ok(response);
        }
    }
}