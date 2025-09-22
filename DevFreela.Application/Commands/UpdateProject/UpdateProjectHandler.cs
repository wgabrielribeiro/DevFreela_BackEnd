﻿using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _context;
    public UpdateProjectHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdFreelancer, cancellationToken); //ficar de olho aqui nesse id

        if (project == null)
            return ResultViewModel.Error("Projeto não encontrado");

        project.Update(request.Title, request.Description, request.TotalCost);

        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}
