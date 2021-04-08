using System;
using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibit
{
    public class FilterExhibitsDto
    {
        public int? MuseumId { get; set; }

        public string Name { get; set; }
        
        public string Author { get; set; }
        
        public DateTime? DateFrom { get; set; }
        
        public DateTime? DateTo { get; set; }
        
        public List<string> Tags { get; set; }
    }
}