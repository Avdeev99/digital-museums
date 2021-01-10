using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Region> Regions { get; set; }
    }
}