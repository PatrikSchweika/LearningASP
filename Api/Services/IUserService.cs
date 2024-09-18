using LearningASP.DTO;
using LearningASP.Model;

namespace LearningASP.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? GetByEmailAndPassword(string email, string password);

    User? Patch(int id, PatchUserDto dto);
}