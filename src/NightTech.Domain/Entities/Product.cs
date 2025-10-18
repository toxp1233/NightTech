namespace NightTech.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;
}
