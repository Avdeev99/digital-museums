using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}