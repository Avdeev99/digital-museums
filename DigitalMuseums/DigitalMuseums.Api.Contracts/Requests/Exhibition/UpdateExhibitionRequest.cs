using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibition
{
    public class UpdateExhibitionRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<string> Tags { get; set; }
        
        public List<int> ExhibitIds { get; set; }
        
        public List<IFormFile> Images { get; set; }
    }
}