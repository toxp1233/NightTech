using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Carts.Querys.GetUserCart;

public class GetUserCartQueryHandler(
    ICartRepository cartRepository,
    IMapper mapper) : IRequestHandler<GetUserCartQuery, CartDto>
{
    public async Task<CartDto> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByGuidAsync(request.CartId) ?? throw new NotFoundException(nameof(Cart), request.CartId.ToString());
        return mapper.Map<CartDto>(cart);
    }
}
