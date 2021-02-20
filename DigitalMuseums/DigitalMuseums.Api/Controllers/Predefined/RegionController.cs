using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers.Predefined
{
    public class RegionController : GenericPredefinedEntityController<Region, BasePredefinedEntityResponse>
    {
        public RegionController(IBasePredefinedEntityService<Region> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }

        [HttpGet("country/{countryId}")]
        public async Task<IActionResult> GetAllByCountry([FromRoute] int countryId)
        {
            var regions = await entityService.GetAllAsync(region => region.CountryId.Equals(countryId));
            var response = mapper.Map<List<BasePredefinedEntityResponse>>(regions);
            return Ok(response);
        }
    }
}