using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetByEmail;
public class LoginByEmailQuery : IRequest<ResultViewModel<LoginViewModel>>
{
    public LoginByEmailQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
