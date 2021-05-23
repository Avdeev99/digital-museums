using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Souvenir
{
    public class UpdateSouvenirRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }
        
        public List<IFormFile> Images { get; set; }
    }
}