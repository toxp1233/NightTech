using FluentValidation;

namespace NightTech.Application.Users.Querys.GetUserByUserName;

public class GetUserByUserNameValidator : AbstractValidator<GetUserByUserNameQuery>
{
    public GetUserByUserNameValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
    }
}
