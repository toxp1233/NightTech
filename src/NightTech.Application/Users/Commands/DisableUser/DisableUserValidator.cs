using FluentValidation;

namespace NightTech.Application.Users.Commands.DisableUser;

public class DisableUserValidator : AbstractValidator<DisableUserCommand>
{
    public DisableUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId cannot be empty.");
    }
}
