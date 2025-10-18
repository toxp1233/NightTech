using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Products.Commands.AddProduct;

public class AddProductCommand : IRequest<ProductDto>
{
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
