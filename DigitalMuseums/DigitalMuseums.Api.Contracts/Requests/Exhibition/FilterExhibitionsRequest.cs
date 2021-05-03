using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibition
{
    public class FilterExhibitionsRequest
    {
        public int? MuseumId { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }
    }
}