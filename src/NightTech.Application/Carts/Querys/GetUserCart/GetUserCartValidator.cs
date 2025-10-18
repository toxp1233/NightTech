using FluentValidation;

namespace NightTech.Application.Carts.Querys.GetUserCart;

public class GetUserCartValidator : AbstractValidator<GetUserCartQuery>
{
    public GetUserCartValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty().WithMessage("CartId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("CartId must be a valid GUID.");
    }
}
