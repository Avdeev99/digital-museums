using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO
{
    public class MuseumItem
    {
        public string Name { get; set; }

        public string Address { get; set; }
        
        public string GenreName { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}