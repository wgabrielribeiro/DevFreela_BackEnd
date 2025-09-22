using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;
public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _context;
    public InsertCommentHandler(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<int>> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject, cancellationToken);

        if (project == null)
            return ResultViewModel<int>.Error("Projeto não encontrado");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
        _context.ProjectsComments.Add(comment);
        await _context.SaveChangesAsync();

        return ResultViewModel<int>.Success(comment.Id);
    }
}
