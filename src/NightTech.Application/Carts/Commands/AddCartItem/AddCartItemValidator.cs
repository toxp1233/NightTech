using FluentValidation;

namespace NightTech.Application.Carts.Commands.AddCartItem;

public class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.CartId).NotEmpty().WithMessage("CartId is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("CartItemId is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
