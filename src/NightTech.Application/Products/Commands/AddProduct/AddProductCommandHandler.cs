using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Products.Commands.AddProduct;

public class AddProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<AddProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        await productRepository.CreateAsync(product);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<ProductDto>(product);
    }
}
