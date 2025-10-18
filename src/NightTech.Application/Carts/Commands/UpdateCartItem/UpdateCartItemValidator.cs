using FluentValidation;

namespace NightTech.Application.Carts.Commands.UpdateCartItem;

public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("CartId is required.");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than zero.");
    }
}
