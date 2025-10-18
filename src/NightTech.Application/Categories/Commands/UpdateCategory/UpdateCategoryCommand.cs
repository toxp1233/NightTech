using MediatR;

namespace NightTech.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Task>
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = default!;
}