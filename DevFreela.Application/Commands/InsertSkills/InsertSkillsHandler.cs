using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkills;

public class InsertSkillsHandler : IRequestHandler<InsertSkillsCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public InsertSkillsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(InsertSkillsCommand request, CancellationToken cancellationToken)
    {
        var model = _userRepository.GetListUsersById(request.Id, request.SkillsIds).Result;

        if (model == null)
        {
            return ResultViewModel.Error("Houve um erro ao inserir as skills do usuário.");
        }

        await _userRepository.PostSkills(model);

        return ResultViewModel.Success();
    }
}
