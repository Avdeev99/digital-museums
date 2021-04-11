using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Souvenir
{
    public class GetFilteredSouvenirsResponseItem
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }

        public string ImagePath { get; set; }
    }
}