using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests.Account;
using DigitalMuseums.Api.Contracts.ViewModels;
using DigitalMuseums.Core.Domain.DTO.Account;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(
            IMapper mapper,
            IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            var changePasswordDto = _mapper.Map<ChangePasswordDto>(changePasswordRequest);

            await _accountService.ChangePasswordAsync(changePasswordDto);

            return Ok();
        }

        [Authorize]
        [HttpPost("user-info")]
        public async Task<IActionResult> EditUserInfoAsync(EditUserInfoRequest editUserInfoRequest)
        {
            await _accountService.EditUserInfoAsync(editUserInfoRequest.Name);

            return Ok();
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _accountService.GetCurrentUser();
            var result = _mapper.Map<UserViewModel>(user);

            return Ok(result);
        }
    }
}
