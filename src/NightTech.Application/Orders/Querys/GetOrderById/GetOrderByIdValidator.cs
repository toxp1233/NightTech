using FluentValidation;

namespace NightTech.Application.Orders.Querys.GetOrderById;

internal class GetOrderByIdValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Order ID must not be empty.");
    }
}
