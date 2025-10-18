using AutoMapper;
using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, Task>
{
    public async Task<Task> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIntIdAsync(request.Id) ?? throw new NotFoundException(nameof(Category), request.Id.ToString());
        mapper.Map(request, category);
        var categoryUpdated = categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return categoryUpdated;
    }
}
