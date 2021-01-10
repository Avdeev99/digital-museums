using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class City
    {
        public int CityId { get; set; }

        public string Name { get; set; }
        
        
        public int RegionId { get; set; }

        public Region Region { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}