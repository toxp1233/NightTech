using FluentValidation;

namespace NightTech.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(RuleFor => RuleFor.Id)
            .NotNull()
            .WithMessage("Id must not be empty");
    }
}
