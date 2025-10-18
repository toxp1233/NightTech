using FluentValidation;

namespace NightTech.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(RuleFor => RuleFor.Id)
            .NotNull()
            .WithMessage("Cannot be Empty");
        RuleFor(RuleFor => RuleFor.CategoryName)
            .NotNull()
            .WithMessage("Cannot be Empty")
            .Length(1, 30)
            .WithMessage("Length: 1 to 30");
    }
}
