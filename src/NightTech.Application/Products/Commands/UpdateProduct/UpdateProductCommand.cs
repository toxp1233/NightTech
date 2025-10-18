using MediatR;

namespace NightTech.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; } 
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
};

