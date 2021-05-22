using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibition
{
    public class CreateExhibitionRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public List<string> Tags { get; set; }

        [Required(AllowEmptyStrings = false)]
        public List<IFormFile> Images { get; set; }
        
        [Required]
        public List<int> ExhibitIds { get; set; }
    }
}