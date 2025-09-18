using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class CreateProjectInputModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdCliente { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }

    public Project ToEntity()
        => new(Title, Description, IdCliente, IdFreelancer, TotalCost);

}
