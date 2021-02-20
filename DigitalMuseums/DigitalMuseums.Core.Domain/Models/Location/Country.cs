using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.Models.Location
{
    public class Country : BasePredefinedEntity
    {
        public ICollection<Region> Regions { get; set; }
    }
}