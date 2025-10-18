using System.Text.Json.Serialization;

namespace NightTech.Application.PublicDtos;

public class CartItemDto
{
    public Guid Id { get; set; }

    // Product link
    public Guid ProductId { get; set; }
    public virtual ProductDto Product { get; set; } = default!;

    // Snapshot info
    public string ProductName { get; set; } = default!;

    public decimal Price { get; set; }

    // User quantity
    public int Quantity { get; set; }
    // FK back to cart
    public Guid CartId { get; set; }
    [JsonIgnore]
    public virtual CartDto Cart { get; set; } = default!;
}
