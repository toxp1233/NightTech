using FluentValidation;

namespace NightTech.Application.Users.Commands.AssignRole;

public class AssignRoleValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId cannot be empty.");
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role cannot be empty.")
            .IsInEnum().WithMessage("User Should be Enum");
    }
}
