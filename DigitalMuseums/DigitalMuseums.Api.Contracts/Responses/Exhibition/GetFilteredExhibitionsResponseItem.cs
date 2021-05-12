using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Exhibition
{
    public class GetFilteredExhibitionsResponseItem
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int MuseumId { get; set; }

        public List<string> Tags { get; set; }

        public string ImagePath { get; set; }
    }
}