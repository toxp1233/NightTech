using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NightTech.Application.Carts.Querys.GetAll;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Carts.Querys.GetAllCarts;

public class GetAllCartsQueryHandler(ICartRepository cartRepository, IMapper mapper) : IRequestHandler<GetAllCartsQuery, PaginatedList<CartDto>>
{
    public async Task<PaginatedList<CartDto>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var cartsQuery = cartRepository.GetQueryable();

        var projected = cartsQuery.ProjectTo<CartDto>(mapper.ConfigurationProvider);

        return await PaginatedList<CartDto>.CreateAsync(
            projected,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize
        );
    }
}
