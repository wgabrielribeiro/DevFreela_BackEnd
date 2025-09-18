namespace DevFreela.Application.Models;

public class UpdateProjectInputModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdCliente { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}
