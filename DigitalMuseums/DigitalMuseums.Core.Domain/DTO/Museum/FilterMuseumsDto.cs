using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Enumerations;

namespace DigitalMuseums.Core.Domain.DTO.Museum
{
    public class FilterMuseumsDto
    {
        public string Name { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? RegionId { get; set; }
        
        public int? CityId { get; set; }

        public List<int> Genres { get; set; }

        public SortingMethod SortingMethod { get; set; }
    }
}