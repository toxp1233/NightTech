using AutoMapper;
using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
        mapper.Map(request, product);
        product.UpdatedAt = DateTime.UtcNow;
        await productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
    }
}
