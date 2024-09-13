using LearningASP.DTO;
using LearningASP.Model;

namespace LearningASP.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User? GetById(int id);

        User? Update(PutUserDto dto);

        User? Patch(int id, PatchUserDto dto);
        
        User Add(CreateUserDto dto);

        User? Remove(int id);
    }
}
