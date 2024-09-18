using LearningASP.Model;

namespace Data.Repositories;

public interface IUserRepository
{
    public IEnumerable<User> GetAll();

    public User? GetById(int id);

    public User? GetByEmailAndPasswordHash(string email, string password);
    public User? GetByEmail(string email);

    public User Create(User user);

    public User? Update(User user);

    public User? Remove(int id);
}