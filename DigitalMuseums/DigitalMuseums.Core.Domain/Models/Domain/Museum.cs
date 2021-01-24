using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Museum : BaseEntity
    {
        public string Name { get; set; }   
        
        public int VisitedCount { get; set; }

        
        public int LocationId { get; set; }

        public Location.Location Location { get; set; }
        
        public int GenreId { get; set; }
        
        public Genre Genre { get; set; }
        
        public ICollection<Image> Images { get; set; }

        public int? UserId { get; set; }
        
        public User User { get; set; }
    }
}