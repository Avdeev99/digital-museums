using System.Collections.Generic;
using DigitalMuseums.Core.Domain.DTO.Exhibit;

namespace DigitalMuseums.Core.Domain.DTO.Exhibition
{
    public class ExhibitionItem
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int MuseumId { get; set; }

        public List<string> Tags { get; set; }

        public List<ExhibitItem> Exhibits { get; set; }
        
        public List<string> ImagePaths { get; set; }
    }
}