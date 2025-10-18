using NightTech.Domain.Entities;

namespace NightTech.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string Email);
}
