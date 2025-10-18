using MediatR;

namespace NightTech.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Task>
{
    public int Id { get; set; }
};
