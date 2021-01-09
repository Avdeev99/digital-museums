using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Auth
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }


        public ICollection<User> Users { get; set; }
    }
}