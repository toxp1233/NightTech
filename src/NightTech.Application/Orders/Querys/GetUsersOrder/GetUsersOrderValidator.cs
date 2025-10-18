using FluentValidation;

namespace NightTech.Application.Orders.Querys.GetUsersOrder;

public class GetUsersOrderValidator : AbstractValidator<GetUsersOrderQuery>
{
    public GetUsersOrderValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
