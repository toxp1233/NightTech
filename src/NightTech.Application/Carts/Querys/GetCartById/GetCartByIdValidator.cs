using FluentValidation;
using NightTech.Application.Carts.Querys.GetCartsById;

namespace NightTech.Application.Carts.Querys.GetCartById;

public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
{
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id is required");
    }
}
