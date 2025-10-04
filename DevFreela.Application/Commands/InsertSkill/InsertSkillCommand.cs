using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill;

public class InsertSkillCommand : IRequest<ResultViewModel>
{
    public string Description { get; set; }


    public Skill FromEntity() => new(Description);

}
