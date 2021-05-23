using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Cart
{
    public class SouvenirItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public int MuseumId { get; set; }

        public List<string> Tags { get; set; }

        public ICollection<string> ImagePaths { get; set; }
    }
}