using LearningASP.DTO;
using LearningASP.Model;

namespace LearningASP.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? Update(UpdateUserDto dto);
}