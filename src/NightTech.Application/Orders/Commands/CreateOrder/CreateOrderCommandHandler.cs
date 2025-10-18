using MediatR;
using NightTech.Application.Orders.Dtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IStripeService stripeService)
    : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Get cart
        var cart = await cartRepository.GetByGuidAsync(request.CartId)
            ?? throw new NotFoundException(nameof(Cart), request.CartId.ToString());

        if (cart.Items == null || cart.Items.Count == 0)
            throw new Exception("Cart is empty.");

        // Step 2: Calculate total
        var totalAmount = cart.Items.Sum(i => i.Price * i.Quantity);

        // Step 3: Begin transaction
        await unitOfWork.BeginTransactionAsync();
        try
        {
            // Step 4: Validate and reduce product stock
            foreach (var item in cart.Items)
            {
                var product = await productRepository.GetByGuidAsync(item.ProductId)
                    ?? throw new NotFoundException(nameof(Product), item.ProductId.ToString());

                if (product.Stock < item.Quantity)
                    throw new Exception($"Not enough stock for product: {product.ProductName}");

                product.Stock -= item.Quantity;
                await productRepository.Update(product);
            }

            // Step 5: Create order
            var order = new Order
            {
                UserId = request.UserId,
                TotalPrice = totalAmount,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            await orderRepository.CreateAsync(order);
            await unitOfWork.SaveChangesAsync();

            // Step 6: Create Stripe Payment Intent
            string clientSecret;
            try
            {
                clientSecret = await stripeService.CreatePaymentIntentAsync(totalAmount, order.Id);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("Stripe payment creation failed. Transaction rolled back.", ex);
            }

            // Step 7: Clear cart and commit transaction
            cart.Items.Clear();
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            // Step 8: Return response
            return new CreateOrderResponse
            {
                OrderId = order.Id,
                ClientSecret = clientSecret
            };
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
