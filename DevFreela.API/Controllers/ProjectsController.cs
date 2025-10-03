using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 0, int size = 3)
        {
            //var result = _projectService.GetAll(search);

            var query = new GetAllProjectsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var result = _projectService.GetById(id);

            var result = _mediator.Send(new GetProjectByIdQuery(id)).Result;

            if (result.IsSuccess == false)
                return BadRequest(result.Message);
            

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertProjectCommand command)
        {
            //var result = _projectService.Insert(model);

            var result = await _mediator.Send(command);

            if ((!result.IsSuccess))
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            //var result = _projectService.Update(value);

            var result = await _mediator.Send(command);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var result = _projectService.Delete(id);

            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            //var result = _projectService.Start(id);

            var result = await _mediator.Send(new StartProjectCommand(id));

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            //var result = _projectService.Complete(id);

            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] InsertCommentCommand command)
        {
            //var result = _projectService.InsertComment(id, model);

            var result = await _mediator.Send(command);

            if (result.IsSuccess == false)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

    }
}
