using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class Location : BaseEntity
    {
        public string Address { get; set; }

        public int CityId { get; set; }
        
        public City City { get; set; }

        public ICollection<Museum> Museums { get; set; }
    }
}