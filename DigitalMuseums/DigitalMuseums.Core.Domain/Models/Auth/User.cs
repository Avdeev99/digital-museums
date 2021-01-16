using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Auth
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public Museum Museum { get; set; }

        public ICollection<Order.Order> Orders { get; set; }
    }
}