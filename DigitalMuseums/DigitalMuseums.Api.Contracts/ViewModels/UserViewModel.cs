using System;

namespace DigitalMuseums.Api.Contracts.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public RoleViewModel Role { get; set; }
    }
}