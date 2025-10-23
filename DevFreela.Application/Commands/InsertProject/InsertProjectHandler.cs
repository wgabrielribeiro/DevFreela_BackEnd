using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{    
    private readonly IMediator _mediator;
    private readonly IProjectRepository _repository;
    public InsertProjectHandler(IProjectRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    public InsertProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        var projectId = await _repository.Add(project);

        if(_mediator is not null)
        {
            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.IdFreelancer);
            await _mediator.Publish(projectCreated, cancellationToken);
        }

        return ResultViewModel<int>.Success(projectId);
    }
}
