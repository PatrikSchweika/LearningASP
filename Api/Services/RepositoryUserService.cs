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

    public User? Update(PutUserDto dto)
    {
        var user = new User(dto.Id, dto.FirstName, dto.LastName);

        return _repository.Update(user);
    }

    public User? Patch(int id, PatchUserDto dto)
    {
        var user = _repository.GetById(id);

        if (user == null) return null;

        var firstName = dto.FirstName ?? user.FirstName;
        var lastName = dto.LastName ?? user.LastName;

        var updatedUser = new User(id, firstName, lastName);

        return _repository.Update(updatedUser);
    }

    public User Add(CreateUserDto dto)
    {
        var user = new User(0, dto.FirstName, dto.LastName);

        return _repository.Create(user);
    }

    public User? Remove(int id)
    {
        return _repository.Remove(id);
    }
}