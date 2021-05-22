using System.Security.Claims;
using DigitalMuseums.Core.Services.Contracts.Providers;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Core.Services.Providers
{
    public class LoggedInPersonProvider : ILoggedInPersonProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInPersonProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId);
        }
    }
}
