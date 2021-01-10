using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Secondary
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Museum> Museums { get; set; }
    }
}