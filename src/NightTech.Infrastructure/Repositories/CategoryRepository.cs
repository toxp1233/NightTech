using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;
using NightTech.Infrastructure.Persistance;

namespace NightTech.Infrastructure.Repositories;

public class CategoryRepository(NightTechDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category entity)
    {
        var addedCategory = await dbContext.Categories.AddAsync(entity);
        return addedCategory.Entity; // no SaveChanges here
    }

    public Task Delete(Category entity)
    {
        dbContext.Categories.Remove(entity);
        return Task.CompletedTask; // no SaveChanges here
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories
            .Include(p => p.Products)
            .ToListAsync();
    }

    public Task<Category?> GetByGuidAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByIntIdAsync(int id)
    {
        return await dbContext.Categories.FindAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await dbContext.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryName == name);
    }

    public IQueryable<Category> GetQueryable()
    {
        return dbContext.Categories
            .Include(c => c.Products)
            .AsQueryable();
    }

    public Task Update(Category entity)
    {
        var updatedEntity = dbContext.Categories.Update(entity);
        return Task.CompletedTask; // no SaveChanges here
    }

}
