using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Orders.Querys.GetOrderById;

public class GetOrderByIdQueryHandler(
    IOrderRepository orderRepository,
    IMapper mapper
    ) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(Order), request.Id.ToString());
        return mapper.Map<OrderDto>(order);
    }
}
