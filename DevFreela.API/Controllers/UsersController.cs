using DevFreela.Application.Commands.InsertSkills;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetByEmail;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
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

        [HttpPut("loginByEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginByEmail(LoginInputModel model)
        {
            //chamar cqrs para validar o login
            var result = await _mediator.Send(new LoginByEmailQuery(model.Email, model.Password));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
