using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class CartItemsRepository(NightTechDbContext dbContext) : ICartItemsRepository
{
    public async Task<CartItem> CreateAsync(CartItem entity)
    {
        await dbContext.CartItems.AddAsync(entity);
        return entity;
    }

    public Task Delete(CartItem entity)
    {
        dbContext.CartItems.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        var items = await dbContext.CartItems.Include(c => c.Cart).ThenInclude(u => u.User).ToListAsync();
        return items;
    }

    public async Task<CartItem?> GetByGuidAsync(Guid id)
    {
       return await dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == id);
        
    }

    public Task<CartItem?> GetByIntIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItem?> GetByNameAsync(string name)
    {
        return await dbContext.CartItems
             .Include(c => c.Cart)
             .ThenInclude(u => u.User)
             .FirstOrDefaultAsync(ci => ci.ProductName == name);
    }

    public IQueryable<CartItem> GetQueryable()
    {
        return dbContext.CartItems
            .Include(c => c.Cart)
            .ThenInclude(u => u.User)
            .AsQueryable();
    }

    public Task Update(CartItem entity)
    {
        dbContext.CartItems.Update(entity);
        return Task.CompletedTask;  
    }
}
