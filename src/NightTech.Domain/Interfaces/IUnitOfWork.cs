namespace NightTech.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
     IUserRepository Users { get; }
    ICartRepository Carts { get; }
    ICartItemsRepository CartItems { get; }
    IOrderRepository Orders { get; }
    IOrderItemsRepository OrderItems { get; }
    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
