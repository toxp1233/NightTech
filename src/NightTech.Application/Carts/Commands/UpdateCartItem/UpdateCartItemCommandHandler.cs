using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Carts.Commands.UpdateCartItem;

public class UpdateCartItemCommandHandler(
    ICartItemsRepository cartItemsRepository,
    ICartRepository cartRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCartItemCommand, CartDto>
{
    public async Task<CartDto> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemsRepository.GetByGuidAsync(request.Id)
            ?? throw new NotFoundException(nameof(CartItem), request.Id.ToString());
        var cart = await cartRepository.GetByGuidAsync(request.CartId)
            ?? throw new NotFoundException(nameof(Cart), request.CartId.ToString());
        var product = await productRepository.GetByGuidAsync(cartItem.ProductId)
            ?? throw new NotFoundException(nameof(Product), cartItem.ProductId.ToString());

        // update quantity
        cartItem.Quantity = request.Quantity;

        if (cartItem.Quantity == 0)
        {
            await cartItemsRepository.Delete(cartItem);
        }
        else
        {
            // ensure price stays as product's unit price
            cartItem.Price = product.Price;
            await cartItemsRepository.Update(cartItem);
        }

        await unitOfWork.SaveChangesAsync();

        var updatedCart = await cartRepository.GetByGuidAsync(cart.Id);
        return mapper.Map<CartDto>(updatedCart);
    }
}
