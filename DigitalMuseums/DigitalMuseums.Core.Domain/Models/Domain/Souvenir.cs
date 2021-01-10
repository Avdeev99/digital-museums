using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Secondary;

namespace DigitalMuseums.Core.Domain.Models.Domain
{
    public class Souvenir
    {
        public int SouvenirId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }


        public ICollection<Image> Images { get; set; }

        public ICollection<Order.Order> Orders { get; set; }
    }
}