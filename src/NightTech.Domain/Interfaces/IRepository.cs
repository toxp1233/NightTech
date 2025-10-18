namespace NightTech.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByGuidAsync(Guid id);
    Task<T?> GetByIntIdAsync(int id);
    Task<T?> GetByNameAsync(string name);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity); 
    Task Update(T entity);       
    Task Delete(T entity);     
    IQueryable<T> GetQueryable();
}
