using System;
using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibit
{
    public class FilterExhibitsRequest
    {
        public string Name { get; set; }
        
        public string Author { get; set; }
        
        public DateTime? DateFrom { get; set; }
        
        public DateTime? DateTo { get; set; }
        
        public List<string> Tags { get; set; }
    }
}