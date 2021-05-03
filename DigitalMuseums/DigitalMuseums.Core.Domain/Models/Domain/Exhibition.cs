using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Interfaces;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Exhibition : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int MuseumId { get; set; }
        
        public Museum Museum { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? AgeLimit { get; set; }
        
        public List<string> Tags { get; set; }

        public List<Exhibit> Exhibits { get; set; }
        
        public List<Image> Images { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}