using DevFreela.API.Models;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelancerTotalCostConfig _config;
        private readonly IConfigService _configService;
        public ProjectsController(IOptions<FreelancerTotalCostConfig> options, IConfigService configService)
        {
            _config = options.Value;
            _configService = configService;            
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new Exception();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectInputModel model)
        {
            if(model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
            {
                return BadRequest($"O custo total deve estar entre {_config.Minimum} e {_config.Maximum}");
            }

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel value)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateProjectCommentInputModel comment)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, comment);
        }

    }
}
