using FluentValidation;

namespace NightTech.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be zero or a positive number.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be a positive number.");
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be a positive integer.");
    }
}
