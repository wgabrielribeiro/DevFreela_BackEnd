using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;
    public UpdateProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.IdFreelancer);

        if (project == null)
            return ResultViewModel.Error("Projeto não encontrado");

        project.Update(project.Title, project.Description, project.TotalCost);

        await _repository.Update(project);

        return ResultViewModel.Success();
    }
}
