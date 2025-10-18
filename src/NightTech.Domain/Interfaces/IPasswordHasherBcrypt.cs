namespace NightTech.Domain.Interfaces;

public interface IPasswordHasherBcrypt
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}