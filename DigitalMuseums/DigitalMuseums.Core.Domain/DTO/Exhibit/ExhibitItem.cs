using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models;

namespace DigitalMuseums.Core.Domain.DTO.Exhibit
{
    public class ExhibitItem
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Author { get; set; }
        
        public DateTime Date { get; set; }
        
        public List<string> Tags { get; set; }

        public ICollection<BasePredefinedEntity> Exhibitions { get; set; }
        
        public ICollection<string> ImagePaths { get; set; }
    }
}