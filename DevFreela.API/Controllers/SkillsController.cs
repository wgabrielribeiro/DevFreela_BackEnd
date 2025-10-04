using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _mediator.Send(new GetAllSkillsQuery());

            return Ok(skills);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand model)
        {
            await _mediator.Send(model);

            return NoContent();
        }

    }
}
