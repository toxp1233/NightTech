using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class OrderRepository(NightTechDbContext dbContext) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order entity)
    {
        var result = await dbContext.Orders.AddAsync(entity);
        return result.Entity;
    }

    public Task Delete(Order entity)
    {
        dbContext.Orders.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
       return await dbContext.Orders.Include(o => o.Items).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid Id)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == Id) 
            .ToListAsync();
    }



    public async Task<Order?> GetByGuidAsync(Guid id)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task<Order?> GetByIntIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Order> GetQueryable()
    {
        return dbContext.Orders.Include(o => o.Items).AsQueryable();
    }

    public Task Update(Order entity)
    {
        dbContext.Orders.Update(entity);
        return Task.CompletedTask;
    }
}
