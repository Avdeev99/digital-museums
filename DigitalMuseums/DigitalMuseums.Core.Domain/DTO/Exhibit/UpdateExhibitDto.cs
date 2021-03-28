using System;
using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Exhibit
{
    public class UpdateExhibitDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Author { get; set; }
        
        public DateTime Date { get; set; }
        
        public List<string> Tags { get; set; }
        
        public ExhibitImagesUnit ImagesData { get; set; }
    }
}