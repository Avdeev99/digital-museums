using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    /// <summary>
    /// Controller for managing user authentication.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("authenticate/google")]
        public IActionResult AuthenticateWithGoogle()
        {
            var result = _authService.AuthenticateWithGoogle(new User());
            return Ok();
        }
    }
}