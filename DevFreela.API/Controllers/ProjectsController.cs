using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var result = _projectService.GetAll(search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);

            if(result.IsSuccess == false)
                return BadRequest(result.Message);
            

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectInputModel model)
        {
            var result = _projectService.Insert(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel value)
        {
            var result = _projectService.Update(value);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _projectService.Delete(id);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _projectService.Start(id);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _projectService.Complete(id);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateProjectCommentInputModel model)
        {
            var result = _projectService.InsertComment(id, model);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

    }
}
