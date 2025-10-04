using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkills;
public class InsertSkillsCommand : IRequest<ResultViewModel>
{
    public int[] SkillsIds { get; set; }
    public int Id { get; set; }

    public InsertSkillsCommand(int[] skillsIds, int id)
    {
        SkillsIds = skillsIds;
        Id = id;
    }
}
