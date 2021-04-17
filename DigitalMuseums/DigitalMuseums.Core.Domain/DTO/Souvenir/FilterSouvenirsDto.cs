using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Enumerations;

namespace DigitalMuseums.Core.Domain.DTO.Souvenir
{
    public class FilterSouvenirsDto
    {
        public int? MuseumId { get; set; }

        public string Name { get; set; }
        
        public decimal? PriceFrom { get; set; }
        
        public decimal? PriceTo { get; set; }

        public List<string> Tags { get; set; }
        
        public SouvenirSortingMethod SortingMethod { get; set; }
    }
}