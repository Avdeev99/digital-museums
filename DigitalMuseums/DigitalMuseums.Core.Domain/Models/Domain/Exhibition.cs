using System;
using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Exhibition : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? AgeLimit { get; set; }
        
        public List<string> Tags { get; set; }

        public ICollection<Exhibit> Exhibits { get; set; }
    }
}