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

    public User? Update(UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }
}