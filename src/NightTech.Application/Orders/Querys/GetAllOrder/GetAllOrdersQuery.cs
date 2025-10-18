using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Orders.Querys.GetAllOrder;

public record GetAllOrdersQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<OrderDto>>;
