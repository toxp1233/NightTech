using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NightTech.Domain.Interfaces;

namespace NightTech.Infrastructure.Repositories;

public class UnitOfWork(NightTechDbContext dbContext) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private ICategoryRepository? _categories;
    private IProductRepository? _products;
    private IUserRepository? _users;
    private ICartRepository? _cart;
    private ICartItemsRepository? _cartItems;
    private IOrderRepository? _orders;
    private IOrderItemsRepository? _orderItems;
    public IUserRepository Users
        => _users ??= new UserRepository(dbContext);
    public ICartRepository Carts 
        => _cart ??= new CartRepository(dbContext);
    public ICategoryRepository Categories
        => _categories ??= new CategoryRepository(dbContext);

    public IProductRepository Products
        => _products ??= new ProductRepository(dbContext);

    public ICartItemsRepository CartItems
        => _cartItems ??= new CartItemsRepository(dbContext);
    public IOrderRepository Orders
        => _orders ??= new OrderRepository(dbContext);
    public IOrderItemsRepository OrderItems
        => _orderItems ??= new OrderItemsRepository(dbContext);

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
    // ✅ Transaction management
    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public void Dispose() => dbContext.Dispose();
}
