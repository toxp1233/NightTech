using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class CartRepository(NightTechDbContext dbContext) : ICartRepository
{
    public async Task<Cart> CreateAsync(Cart entity)
    {
        var cart = await dbContext.Carts.AddAsync(entity);
        return cart.Entity; // no SaveChanges here
    }

    public Task Delete(Cart entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await dbContext.Carts.Include(u => u.User).Include(c => c.Items!).ToListAsync(); 
    }

    public async Task<Cart?> GetByGuidAsync(Guid id)
    {
        return await dbContext.Carts
            .Include(c => c.Items!)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Cart?> GetByIntIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Cart?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId)
    {
        return await dbContext.Carts
            .Include(c => c.User)
            .Include(c => c.Items!)
            .ThenInclude(ci => ci.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public IQueryable<Cart> GetQueryable()
    {
        return dbContext.Carts.Include(c => c.User)
            .Include(c => c.Items!).AsQueryable();
    }

    public Task Update(Cart entity)
    {
        var cart = dbContext.Carts.Update(entity);
        return Task.CompletedTask; // no SaveChanges here
    }
}
