using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Orders.Querys.GetUsersOrder;

public class GetUsersOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetUsersOrderQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetUsersOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllByUserIdAsync(request.UserId) ?? throw new NotFoundException(nameof(Order), request.UserId.ToString());
        return mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
