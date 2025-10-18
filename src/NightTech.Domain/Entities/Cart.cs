namespace NightTech.Domain.Entities;

public class Cart
{
    public Guid Id { get; set; }

    // Link to the user who owns the cart
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

    // Calculated fields
    public decimal? TotalPrice { get; set; }
    public int? ItemCount { get; set; }

    // Collection of items in the cart
    public virtual ICollection<CartItem>? Items { get; set; } = [];
}
