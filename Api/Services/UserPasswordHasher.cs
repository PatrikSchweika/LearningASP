using System.Security.Cryptography;

using LearningASP.Model;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace LearningASP.Services;

public class UserPasswordHasher : IPasswordHasher<User>
{
    public string HashPassword(User user, string password)
    {
        return HashPassword(user.Salt, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string password)
    {
        var passwordHash = HashPassword(user.Salt, password);

        var equalHash = AreHashesEqual(hashedPassword, passwordHash);

        return equalHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    private bool AreHashesEqual(string hash1, string hash2)
    {
        return hash1.Equals(hash2);
    }

    private string HashPassword(string salt, string password)
    {
        var saltBytes = Convert.FromBase64String(salt);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashed;
    }
}