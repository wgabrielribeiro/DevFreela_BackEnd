using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public InsertUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.FromEntity();
        await _userRepository.Post(user);

        return ResultViewModel.Success();
    }
}
