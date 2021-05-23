using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Exhibition
{
    public class UpdateExhibitionRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        [Required]
        public List<int> ExhibitIds { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}