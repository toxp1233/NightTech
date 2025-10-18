namespace NightTech.Application.PublicDtos;

public class CartDto
{
    public Guid Id { get; set; }

    // Link to the user who owns the cart
    public Guid UserId { get; set; }
    public virtual UserDto User { get; set; } = default!;

    // Calculated fields
    public decimal? TotalPrice { get; set; }
    public int? ItemCount { get; set; }

    // Collection of items in the cart
    public virtual ICollection<CartItemDto>? Items { get; set; } = [];
}
