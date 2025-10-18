using FluentValidation;

namespace NightTech.Application.Categories.Querys.GetCategoryById;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Cannot be Empty");
    }
}
