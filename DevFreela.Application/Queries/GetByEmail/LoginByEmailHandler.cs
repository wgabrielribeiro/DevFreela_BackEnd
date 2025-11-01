using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetByEmail;
public class LoginByEmailHandler : IRequestHandler<LoginByEmailQuery, ResultViewModel<LoginViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginByEmailHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }
    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginByEmailQuery request, CancellationToken cancellationToken)
    {      
        request.Password = _authService.ComputeHash(request.Password);

        var user = await _userRepository.LoginByEmail(request.Email, request.Password);

        if (user == null)
        {
            return ResultViewModel<LoginViewModel>.Error("Invalid email or password");
        }

        var token = _authService.GenerateToken(user.Email, user.Role);

        var loginViewModel = new LoginViewModel(token);

        return ResultViewModel<LoginViewModel>.Success(loginViewModel);
    }
}
