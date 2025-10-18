using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Products.Querys.GetAllProducts;

public record GetAllProductsQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<ProductDto>>;
