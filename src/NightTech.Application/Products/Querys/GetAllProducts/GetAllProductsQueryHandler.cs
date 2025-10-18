using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Products.Querys.GetAllProducts;

public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, PaginatedList<ProductDto>>
{
    public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = productRepository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.PaginationParams.Search))
        {
            var search = request.PaginationParams.Search.ToLower();
            productsQuery = productsQuery
                .Where(p => p.Category != null &&
                            p.Category.CategoryName.ToLower().Contains(search));
        }

        var projected = productsQuery.ProjectTo<ProductDto>(mapper.ConfigurationProvider);

        return await PaginatedList<ProductDto>.CreateAsync(
            projected,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize
        );
    }
}
