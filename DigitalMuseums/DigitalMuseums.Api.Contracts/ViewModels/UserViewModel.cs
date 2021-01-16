using System;
using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Api.Contracts.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public Role Role { get; set; }
    }
}