namespace NightTech.Application.PublicDtos;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;  // snapshot name
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
