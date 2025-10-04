﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;
    public CompleteProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.Id);

        if (project == null)
            return ResultViewModel.Error("Projeto não encontrado");

        project.Complete();
        await _repository.Update(project);

        return ResultViewModel.Success();
    }
}
