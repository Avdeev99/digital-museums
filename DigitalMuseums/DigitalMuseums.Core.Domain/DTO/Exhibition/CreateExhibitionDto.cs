using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibition
{
    public class CreateExhibitionDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        
        public int MuseumId { get; set; }
        
        public ExhibitionImagesUnit ImagesData { get; set; }

        public List<int> ExhibitIds { get; set; }
    }
}