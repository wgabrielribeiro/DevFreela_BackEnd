using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult Put(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";
            
            return Ok(description);
        }
    }
}
