using NightTech.Domain.Entities;

namespace NightTech.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(Guid userId);
}
