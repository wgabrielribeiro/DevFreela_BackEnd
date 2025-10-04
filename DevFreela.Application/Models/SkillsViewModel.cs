using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class SkillsViewModel
{
    public SkillsViewModel(int id, string description)
    {
        Description = description;
    }
    public string Description { get; private set; }

    public static SkillsViewModel FromEntity(Skill skill)
    {
        return new SkillsViewModel(skill.Id, skill.Description);
    }

}
