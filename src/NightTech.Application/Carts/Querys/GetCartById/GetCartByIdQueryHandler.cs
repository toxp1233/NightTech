using AutoMapper;
using MediatR;
using NightTech.Application.Carts.Querys.GetCartsById;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Carts.Querys.GetCartById;

public class GetCartByIdQueryHandler(ICartRepository cartRepository, IMapper mapper) : IRequestHandler<GetCartByIdQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(Cart), request.Id.ToString());
        return mapper.Map<CartDto>(cart);
    }
}
