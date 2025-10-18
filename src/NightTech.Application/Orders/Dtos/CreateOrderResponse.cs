namespace NightTech.Application.Orders.Dtos;

public class CreateOrderResponse
{
    public Guid OrderId { get; set; }
    public string ClientSecret { get; set; } = string.Empty;
}
