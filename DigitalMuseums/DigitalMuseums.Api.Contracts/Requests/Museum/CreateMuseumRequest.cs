using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalMuseums.Api.Contracts.Requests.Museum
{
    public class CreateMuseumRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public int CityId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int GenreId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public List<IFormFile> Images { get; set; }
    }
}