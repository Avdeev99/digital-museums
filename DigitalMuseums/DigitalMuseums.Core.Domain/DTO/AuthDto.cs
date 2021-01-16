using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Core.Domain.DTO
{
    public class AuthDto
    {
        public string Token { get; set; }
        
        public User User { get; set; }
    }
}