using FluentValidation;

namespace NightTech.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(RuleFor => RuleFor.CategoryName)
            .NotNull()
            .WithMessage("Cannot be Empty")
            .Length(1, 30)
            .WithMessage("Length: 1 to 30");
    }
}
