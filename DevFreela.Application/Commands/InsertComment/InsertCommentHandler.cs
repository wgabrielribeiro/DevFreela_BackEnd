using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment;
public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel<int>>
{
    private readonly IProjectRepository _repository;
    public InsertCommentHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<int>> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var existsProject = await _repository.Exists(request.IdProject);

        if (!existsProject)
            return ResultViewModel<int>.Error("Projeto não encontrado");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

        await _repository.AddComment(comment);

        return ResultViewModel<int>.Success(comment.Id);
    }
}
