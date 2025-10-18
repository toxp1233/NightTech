using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Orders.Querys.GetUsersOrder;

public record GetUsersOrderQuery(Guid UserId) : IRequest<IEnumerable<OrderDto>>;
