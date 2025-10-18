using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Orders.Querys.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto>;
