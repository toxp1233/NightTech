using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Constants;

namespace NightTech.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid OrderId, OrderStatus OrderStatus) : IRequest<OrderDto>;
