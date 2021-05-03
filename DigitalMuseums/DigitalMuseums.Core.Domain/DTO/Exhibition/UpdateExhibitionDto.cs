using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibition
{
    public class UpdateExhibitionDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        
        public List<int> Exhibits { get; set; }
        
        public ExhibitionImagesUnit ImagesData { get; set; }
    }
}