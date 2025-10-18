using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Products.Querys.GetProductsById;

public record GetProductsByIdQuery(Guid Id) : IRequest<ProductDto>;
