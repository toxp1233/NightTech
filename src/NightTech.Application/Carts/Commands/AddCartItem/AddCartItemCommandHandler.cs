using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Carts.Commands.AddCartItem;

public class AddCartItemCommandHandler(ICartRepository cartRepository,
    ICartItemsRepository cartItemsRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<AddCartItemCommand, CartDto>
{
    public async Task<CartDto> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByGuidAsync(request.CartId) ?? throw new NotFoundException(nameof(Cart), request.CartId.ToString());
        var product = await productRepository.GetByGuidAsync(request.ProductId) ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        var cartItem = await cartItemsRepository.CreateAsync(new CartItem
        {
            ProductName = product.ProductName,
            CartId = cart.Id,
            ProductId = product.Id,
            Price = product.Price,
            Quantity = request.Quantity
        });
        await unitOfWork.SaveChangesAsync();
        var updatedCart = await cartRepository.GetByGuidAsync(cart.Id);
        return mapper.Map<CartDto>(updatedCart);

    }
}
