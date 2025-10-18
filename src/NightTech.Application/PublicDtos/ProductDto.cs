namespace NightTech.Application.PublicDtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual CategoryDto Category { get; set; } = default!;
}
