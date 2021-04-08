using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibit
{
    public class CreateExhibitRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Author { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public DateTime Date { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public List<string> Tags { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int MuseumId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public List<IFormFile> Images { get; set; }
    }
}