using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Exhibit
    {
        public int ExhibitId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Author { get; set; }
        
        public DateTime Date { get; set; }
        
        public List<string> Tags { get; set; }


        public ICollection<Image> Images { get; set; }
        
        public ICollection<Exhibition> Exhibitions { get; set; }
    }
}