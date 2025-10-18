using MediatR;

namespace NightTech.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;
