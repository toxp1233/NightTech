using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Carts.Commands.UpdateCartItem;

public class UpdateCartItemCommand : IRequest<CartDto>
{
    public Guid CartId { get; set; }
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}
