using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;
        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted)
                //.Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                //.Skip(page * size)
                //.Take(size)
                .ToListAsync();
        }

        public async Task<Project?> GetById(int id)
        {
            return await _context.Projects
                            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            return await _context.Projects
                            .Include(p => p.Client)
                            .Include(p => p.Freelancer)
                            .Include(p => p.Comments)
                            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> Add(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task AddComment(ProjectComment comment)
        {
            _context.ProjectsComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }
        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
