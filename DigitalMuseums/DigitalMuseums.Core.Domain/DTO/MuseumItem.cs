using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Location;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.DTO
{
    public class MuseumItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public Genre Genre { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}