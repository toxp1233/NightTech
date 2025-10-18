using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class OrderItemsRepository(NightTechDbContext dbContext) : IOrderItemsRepository
{
    public async Task<OrderItem> CreateAsync(OrderItem entity)
    {
        var result = await dbContext.OrderItems.AddAsync(entity);
        return result.Entity;
    }

    public Task Delete(OrderItem entity)
    {
        dbContext.OrderItems.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await dbContext.OrderItems.Include(o => o.Order).ToListAsync();
    }

    public async Task<OrderItem?> GetByGuidAsync(Guid id)
    {
        return await dbContext.OrderItems
            .Include(o => o.Order)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task<OrderItem?> GetByIntIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderItem?> GetByNameAsync(string name)
    {
        return await dbContext.OrderItems
            .Include(o => o.Order)
            .FirstOrDefaultAsync(o => o.ProductName == name);
    }

    public IQueryable<OrderItem> GetQueryable()
    {
        return dbContext.OrderItems.Include(o => o.Order).AsQueryable();
    }

    public Task Update(OrderItem entity)
    {
        dbContext.OrderItems.Update(entity);
        return Task.CompletedTask;
    }
}
