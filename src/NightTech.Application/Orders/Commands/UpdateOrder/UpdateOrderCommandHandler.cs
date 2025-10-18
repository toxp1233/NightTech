using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Constants;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Orders.Commands.UpdateOrder;


public class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByGuidAsync(request.OrderId)
            ?? throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        // Optional: prevent invalid transitions
        if (order.Status == OrderStatus.Completed && request.OrderStatus != OrderStatus.Completed)
            throw new Exception("Cannot modify a completed order.");

        order.Status = request.OrderStatus;

        await unitOfWork.SaveChangesAsync();

        return mapper.Map<OrderDto>(order);
    }
};