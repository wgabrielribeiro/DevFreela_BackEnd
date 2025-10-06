using DevFreela.Application.Commands.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators;
public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
{
    public InsertProjectValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("O título do projeto é obrigatório.")
            .MaximumLength(50)
            .WithMessage("O título do projeto deve ter no máximo 50 caracteres.");

        RuleFor(c => c.TotalCost)
            .GreaterThan(0)
            .WithMessage("O custo total deve ser maior que zero.");
    }
}
