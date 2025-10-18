using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Services;

public class PasswordHasher : IPasswordHasherBcrypt
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
