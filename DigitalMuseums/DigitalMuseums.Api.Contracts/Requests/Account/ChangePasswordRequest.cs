using System.ComponentModel.DataAnnotations;

namespace DigitalMuseums.Api.Contracts.Requests.Account
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirmation { get; set; }
    }
}
