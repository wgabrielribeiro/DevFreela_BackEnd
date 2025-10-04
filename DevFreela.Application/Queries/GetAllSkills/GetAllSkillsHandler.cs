using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsCommand : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillsViewModel>>>
{
    private readonly ISkillRepository _repository;
    public GetAllSkillsCommand(ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<SkillsViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skillsList = await _repository.GetAll();

        var skillsViewModel = skillsList.Select(SkillsViewModel.FromEntity).ToList();

        return ResultViewModel<List<SkillsViewModel>>.Success(skillsViewModel);
    }
}
