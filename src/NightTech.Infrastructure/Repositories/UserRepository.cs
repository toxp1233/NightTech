using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class UserRepository(NightTechDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateAsync(User entity)
    {
        await dbContext.Users.AddAsync(entity);
        return entity; // no SaveChanges here
    }

    public Task Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await dbContext.Users
            .AsNoTracking()
            .Include(c => c.Cart)
            .ToListAsync();
    }

    public Task<User?> GetByEmailAsync(string Email)
    {
        return dbContext.Users
            .Include(c => c.Cart)
            .FirstOrDefaultAsync(u => u.Email == Email);
    }

    public async Task<User?> GetByGuidAsync(Guid id)
    {
        var user = await dbContext.Users
            .Include(c => c.Cart)
            .FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public Task<User?> GetByIntIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        var user = await dbContext.Users
            .Include(c => c.Cart)
            .FirstOrDefaultAsync(u => u.UserName == name);
        return user;
    }

    public IQueryable<User> GetQueryable()
    {
        var users = dbContext.Users
            .Include(c => c.Cart)
            .AsQueryable();
        return users;
    }

    public Task Update(User entity)
    {
        dbContext.Users.Update(entity);
        return Task.CompletedTask;
    }
}
