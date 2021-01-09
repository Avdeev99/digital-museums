using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class Country
    {
        public int CountryId { get; set; }
        
        public string Name { get; set; }


        public ICollection<Region> Regions { get; set; }
    }
}