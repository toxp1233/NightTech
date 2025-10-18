using FluentValidation;

namespace NightTech.Application.Products.Querys.GetProductsById;

public class GetProductsByIdQueryValidator : AbstractValidator<GetProductsByIdQuery>
{
    public GetProductsByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}
