using DevFreela.Application.Commands.InsertUser;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Data;

namespace DevFreela.Application.Validators;

public class CreateUserValidator : AbstractValidator<InsertUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("O email é inválido.");

        RuleFor(u => u.BirthDate)
            .Must(d => d < DateTime.Now.AddYears(-18))
            .WithMessage("O usuário deve ter pelo menos 18 anos.");
    }
}
