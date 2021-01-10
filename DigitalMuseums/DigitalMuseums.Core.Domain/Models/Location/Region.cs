using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        
        public int CountryId { get; set; }
        
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}