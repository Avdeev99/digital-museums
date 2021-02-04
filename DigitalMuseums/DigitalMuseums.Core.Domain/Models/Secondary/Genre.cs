using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Secondary
{
    public class Genre : BasePredefinedEntity
    {
        public ICollection<Museum> Museums { get; set; }
    }
}