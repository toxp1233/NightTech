using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;
using NightTech.Infrastructure.Persistance;

namespace NightTech.Infrastructure.Repositories;

public class ProductRepository(NightTechDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateAsync(Product entity)
    {
        var addedEntity = await dbContext.Products.AddAsync(entity);
        return addedEntity.Entity; 
    }

    public Task Delete(Product entity)
    {
        dbContext.Products.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products
            .AsNoTracking()
            .Include(c => c.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByGuidAsync(Guid id)
    {
        return await dbContext.Products
            .Include(c => c.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Product?> GetByIntIdAsync(int id)
    {
        // Assuming Product.Id is Guid, this may not be needed
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await dbContext.Products
            .Include(c => c.Category)
            .FirstOrDefaultAsync(p => p.ProductName == name);
    }

    public IQueryable<Product> GetQueryable()
    {
        return dbContext.Products
            .Include(c => c.Category)
            .AsQueryable();
    }

    public Task Update(Product entity)
    {
        dbContext.Products.Update(entity);
        return Task.CompletedTask;
    }
}
