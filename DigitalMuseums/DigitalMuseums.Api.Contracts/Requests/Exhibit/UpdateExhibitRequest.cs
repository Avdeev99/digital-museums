using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibit
{
    public class UpdateExhibitRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Author { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public DateTime Date { get; set; }
        
        public List<string> Tags { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}