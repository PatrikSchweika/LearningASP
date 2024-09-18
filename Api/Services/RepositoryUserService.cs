using Data.Repositories;

using LearningASP.DTO;
using LearningASP.Model;

namespace LearningASP.Services;

public class RepositoryUserService : IUserService
{
    private readonly IUserRepository _repository;

    public RepositoryUserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<User> GetAll()
    {
        return _repository.GetAll();
    }

    public User? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        throw new NotImplementedException();
    }

    public User? Patch(int id, PatchUserDto dto)
    {
        var user = _repository.GetById(id);

        if (user == null) return null;

        var firstName = dto.FirstName ?? user.FirstName;
        var lastName = dto.LastName ?? user.LastName;

        var updatedUser = new User(id, user.Email, user.Password, firstName, lastName);

        return _repository.Update(updatedUser);
    }
}