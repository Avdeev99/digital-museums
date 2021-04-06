using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Souvenir
{
    public class GetSouvenirResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }

        public ICollection<string> ImagePaths { get; set; }
    }
}