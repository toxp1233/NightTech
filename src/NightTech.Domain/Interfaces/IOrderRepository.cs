using NightTech.Domain.Entities;

namespace NightTech.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllByUserIdAsync(Guid Id);
}
