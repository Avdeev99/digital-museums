using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers
{
    [ApiController]
    [Route("/api/exhibition")]
    public class ExhibitionController : Controller
    {
        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }
        
        [HttpGet]
        public IActionResult GetAll() // filter
        {
            return Ok();
        }
        
        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}