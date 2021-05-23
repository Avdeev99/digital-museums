using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Account
{
    public class EditUserInfoRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
