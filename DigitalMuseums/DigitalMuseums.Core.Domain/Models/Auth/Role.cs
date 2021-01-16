using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Auth
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }


        public ICollection<User> Users { get; set; }
    }
}