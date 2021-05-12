using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibition
{
    public class FilterExhibitionsDto
    {
        public int? MuseumId { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }
    }
}