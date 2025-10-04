using DevFreela.Application.Commands.InsertSkills;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(InsertUserCommand model)
        {
            await _mediator.Send(model);

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkillsAsync(int id, UserSkillsInputModel model)
        {
            var isSuccess = await _mediator.Send(new InsertSkillsCommand(model.SkillsIds, id));

            if(!isSuccess.IsSuccess)
            {
                return BadRequest(isSuccess.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult Put(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            return Ok(description);
        }
    }
}
