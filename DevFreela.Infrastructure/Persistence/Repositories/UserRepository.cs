using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _context;
    public UserRepository(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetById(int id)
    {
        return await _context.Users.
                        Include(u => u.Skills).
                        ThenInclude(us => us.Skill).
                        SingleOrDefaultAsync(u => u.Id == id);
    }
    public Task<List<UserSkill>> GetListUsersById(int id, int[] listIds)
    {
        var users = listIds
             .Select(s => new UserSkill(id, s)).ToList();

        return Task.FromResult(users);
    }

    public async Task Post(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task PostSkills(List<UserSkill> userSkills)
    {
        _context.UserSkills.AddRange(userSkills);
        await _context.SaveChangesAsync();
    }
}
