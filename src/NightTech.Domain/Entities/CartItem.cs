namespace NightTech.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }

    // Product link
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;

    // Snapshot info
    public string ProductName { get; set; } = default!; 

    public decimal Price { get; set; }

    // User quantity
    public int Quantity { get; set; }
    // FK back to cart
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; } = default!;
}
