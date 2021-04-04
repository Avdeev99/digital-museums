using System;
using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibit
{
    public class CreateExhibitDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Author { get; set; }
        
        public DateTime Date { get; set; }
        
        public List<string> Tags { get; set; }
        
        public int MuseumId { get; set; }
        
        public ExhibitImagesUnit ImagesData { get; set; }
    }
}