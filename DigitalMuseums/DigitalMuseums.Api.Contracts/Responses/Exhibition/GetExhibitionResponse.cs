using System.Collections.Generic;
using DigitalMuseums.Api.Contracts.Responses.Exhibit;

namespace DigitalMuseums.Api.Contracts.Responses.Exhibition
{
    public class GetExhibitionResponse
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int MuseumId { get; set; }

        public List<string> Tags { get; set; }

        public List<GetExhibitResponse> Exhibits { get; set; }
        
        public List<string> ImagePaths { get; set; }
    }
}