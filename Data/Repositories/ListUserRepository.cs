﻿using LearningASP.Model;

namespace Data.Repositories;

public class ListUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetByEmailAndPasswordHash(string email, string password)
    {
        return _users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
    }

    public User? GetByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public User Create(User user)
    {
        var userWithId = user with { Id = _nextId++ };

        _users.Add(userWithId);

        return userWithId;
    }

    public User? Update(User user)
    {
        var index = _users.FindIndex(userA => userA.Id == user.Id);

        if (index == -1) return null;

        _users[index] = user;

        return user;
    }

    public User? Remove(int id)
    {
        var index = _users.FindIndex(userA => userA.Id == id);

        if (index == -1) return null;

        var user = _users[index];

        _users.RemoveAt(index);

        return user;
    }
}