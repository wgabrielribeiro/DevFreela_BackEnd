﻿using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _context;
    private readonly IMediator _mediator;
    public InsertProjectHandler(DevFreelaDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.IdFreelancer);
        await _mediator.Publish(projectCreated, cancellationToken);

        return ResultViewModel<int>.Success(project.Id);
    }
}
