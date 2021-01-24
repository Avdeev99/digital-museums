using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers.Predefined
{
    public class CityController : GenericPredefinedEntityController<City, BasePredefinedEntityResponse>
    {
        public CityController(IBasePredefinedEntityService<City> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
        
        [HttpGet("region/{regionId}")]
        public async Task<IActionResult> GetAllByCountry([FromRoute] int regionId)
        {
            var regions = await entityService.GetAllAsync(city => city.RegionId.Equals(regionId));
            var response = mapper.Map<List<BasePredefinedEntityResponse>>(regions);
            return Ok(response);
        }
    }
}