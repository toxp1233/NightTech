using MediatR;
using NightTech.Application.Orders.Dtos;

namespace NightTech.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid UserId, Guid CartId) : IRequest<CreateOrderResponse>;
