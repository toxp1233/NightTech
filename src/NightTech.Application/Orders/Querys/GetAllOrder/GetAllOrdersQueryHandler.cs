using AutoMapper;
using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Orders.Querys.GetAllOrder;

public class GetAllOrdersQueryHandler(
    IOrderRepository orderRepository,
    IMapper mapper) : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderDto>>
{
    public async Task<PaginatedList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = orderRepository.GetQueryable();
        var orderlist = mapper.ProjectTo<OrderDto>(orders);
        return await PaginatedList<OrderDto>.CreateAsync(orderlist, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
    }
}
