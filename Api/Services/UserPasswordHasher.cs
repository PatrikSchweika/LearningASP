using System.Security.Cryptography;

using LearningASP.Model;

using Microsoft.AspNetCore.Identity;

namespace LearningASP.Services;

public class UserPasswordHasher : IPasswordHasher<User>
{
    public string HashPassword(User user, string password)
    {
        return HashPassword(password);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string password)
    {
        var passwordHash = HashPassword(password);

        var equalHash = AreHashesEqual(hashedPassword, passwordHash);

        return equalHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    private bool AreHashesEqual(string hash1, string hash2)
    {
        return hash1.Equals(hash2);
    }

    private string HashPassword(string password)
    {
        return password.Reverse().ToString()!;
    }
}