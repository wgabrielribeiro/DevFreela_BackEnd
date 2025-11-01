using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;
public class InsertUserCommand : IRequest<ResultViewModel>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User FromEntity()
        => new User(FullName, Email, BirthDate, Password, Role);
}
