using FluentValidation;

namespace NightTech.Application.Categories.Querys.GetCategoryByName;

public class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Cannot be Empty")
            .Length(1, 30)
            .WithMessage("Length: 1 to 30");
    }
}
