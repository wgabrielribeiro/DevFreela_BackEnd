using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _context;
    public SkillRepository(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<List<Skill>> GetAll()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task Post(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

    }
}
