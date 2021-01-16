using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Contracts.Requests;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Auth.Options;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Services.Contracts;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        private readonly IMapper _mapper;
        private readonly GoogleAuthOptions _googleAuthOptions;
        
        public AuthController(
            IAuthService authService,
            IMapper mapper,
            IOptions<GoogleAuthOptions> googleAuthOptions)
        {
            _authService = authService;
            _mapper = mapper;
            _googleAuthOptions = googleAuthOptions.Value;
        }

        /// <summary>
        /// Authenticate finder with google.
        /// </summary>
        /// <param name="googleAuth">The google authentication request model.</param>
        /// <returns>The jwt token.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("authenticate/google")]
        public async Task<IActionResult> AuthenticateWithGoogle([FromBody] GoogleAuthRequest googleAuth)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _googleAuthOptions.ClientId },
            };

            var googlePayload = await GoogleJsonWebSignature.ValidateAsync(googleAuth.IdToken, settings);

            if (googlePayload == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(googlePayload);
            var authResult = await _authService.AuthenticateWithGoogle(user);
            var response = _mapper.Map<AuthResponse>(authResult);

            return Ok(response);
        }
    }
}