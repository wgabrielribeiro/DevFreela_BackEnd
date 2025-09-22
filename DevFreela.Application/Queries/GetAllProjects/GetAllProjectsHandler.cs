using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly DevFreelaDbContext _context;
    public GetAllProjectsHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted)
                //.Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                //.Skip(page * size)
                //.Take(size)
                .ToListAsync(cancellationToken);

        var model = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}
