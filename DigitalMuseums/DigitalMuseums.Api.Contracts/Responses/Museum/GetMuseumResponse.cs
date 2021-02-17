using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Museum
{
    public class GetMuseumResponse
    {
        public string Name { get; set; }

        public string Address { get; set; }
        
        public string GenreName { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}