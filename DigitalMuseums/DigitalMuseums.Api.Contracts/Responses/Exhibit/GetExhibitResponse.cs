using System;
using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Exhibit
{
    public class GetExhibitResponse
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Author { get; set; }
        
        public DateTime Date { get; set; }

        public int MuseumId { get; set; }

        public List<string> Tags { get; set; }

        public ICollection<BasePredefinedEntityResponse> Exhibitions { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}