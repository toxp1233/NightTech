using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Task>
{
    public async Task<Task> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIntIdAsync(request.Id) ?? throw new NotFoundException(nameof(Category), request.Id.ToString());
        await categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return Task.CompletedTask;
    }
}
