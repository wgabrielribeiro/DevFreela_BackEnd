using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;
public interface IUserRepository
{
    Task<User> GetById(int id);
    Task<List<UserSkill>> GetListUsersById(int id, int[] listIds);
    Task Post(User user);
    Task PostSkills(List<UserSkill> userSkills);
    Task<User> LoginByEmail(string email, string password);

}
