using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Products.Querys.GetProductsById;

public class GetProductsByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductsByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
        return mapper.Map<ProductDto>(product);
    }
}
