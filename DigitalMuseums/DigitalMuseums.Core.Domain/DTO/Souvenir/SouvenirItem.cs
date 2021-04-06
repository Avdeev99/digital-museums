using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Souvenir
{
    public class SouvenirItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }

        public ICollection<string> ImagePaths { get; set; }
    }
}