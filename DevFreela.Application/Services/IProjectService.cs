using Azure;
using DevFreela.Application.Models;
using System.Drawing;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string? query = "");
        ResultViewModel<ProjectViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateProjectInputModel model);
        ResultViewModel Update(UpdateProjectInputModel model);
        ResultViewModel Start(int id);
        ResultViewModel Delete(int id);
        ResultViewModel Complete(int id);
        ResultViewModel<int> InsertComment (int id, CreateProjectCommentInputModel model);
    }
}
