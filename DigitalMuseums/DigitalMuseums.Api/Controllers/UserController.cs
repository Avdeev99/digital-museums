using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("base/list")]
        public async Task<IActionResult> GetBaseListAsync()
        {
            var users = await _userService.GetBaseListAsync();
            var result = _mapper.Map<List<BasePredefinedEntityResponse>>(users);

            return Ok(result);
        }
    }
}
