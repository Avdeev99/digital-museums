using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Museum
{
    public class GetMuseumResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public BasePredefinedEntityResponse Country { get; set; }

        public BasePredefinedEntityResponse Region { get; set; }

        public BasePredefinedEntityResponse City { get; set; }

        public string Address { get; set; }
        
        public BasePredefinedEntityResponse Genre { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}