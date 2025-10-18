using FluentValidation;

namespace NightTech.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order Id is required.");
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Invalid order status.");
    }
}
