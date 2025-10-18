using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Carts.Querys.GetCartsById;

public record GetCartByIdQuery(Guid Id) : IRequest<CartDto>;
