using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService authService;

    public InsertUserHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        this.authService = authService;
    }

    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        request.Password = this.authService.ComputeHash(request.Password);
        var user = request.FromEntity();
        await _userRepository.Post(user);

        return ResultViewModel.Success();
    }
}
