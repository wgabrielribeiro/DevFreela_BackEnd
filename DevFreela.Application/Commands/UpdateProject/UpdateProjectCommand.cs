using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;
public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdCliente { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}
