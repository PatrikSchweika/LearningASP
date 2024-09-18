using LearningASP.Model;

namespace Data.Repositories;

public class DbUserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public DbUserRepository(UserDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
    }

    public User Create(User user)
    {
        var addedUser = _context.Users.Add(user);

        _context.SaveChanges();

        return addedUser.Entity;
    }

    public User? Update(User user)
    {
        var updatedUser = _context.Users.Update(user);


        _context.SaveChanges();

        return updatedUser.Entity;
    }

    public User? Remove(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null) return null;

        _context.Users.Remove(user);

        _context.SaveChanges();

        return user;
    }
}