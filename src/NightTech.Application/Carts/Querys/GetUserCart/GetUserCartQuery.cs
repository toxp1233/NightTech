using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Carts.Querys.GetUserCart;

public record GetUserCartQuery(Guid CartId) : IRequest<CartDto>;
