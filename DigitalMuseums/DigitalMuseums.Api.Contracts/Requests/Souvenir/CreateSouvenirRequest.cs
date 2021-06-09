using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Souvenir
{
    public class CreateSouvenirRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int AvailableUnits { get; set; }

        public List<string> Tags { get; set; }
        
        [Required]
        public int MuseumId { get; set; }
        
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}