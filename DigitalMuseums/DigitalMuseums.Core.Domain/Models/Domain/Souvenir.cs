using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Interfaces;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Souvenir : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }
        
        public int MuseumId { get; set; }

        public Museum Museum { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<SouvenirOrderDetail> OrderDetails { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}