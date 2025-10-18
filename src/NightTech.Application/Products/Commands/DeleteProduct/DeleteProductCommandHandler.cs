using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
        await productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
    }
}
