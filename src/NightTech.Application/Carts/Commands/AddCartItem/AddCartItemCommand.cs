using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Carts.Commands.AddCartItem;

public class AddCartItemCommand : IRequest<CartDto>
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}