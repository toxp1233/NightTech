using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);
        var createdCategory = await categoryRepository.CreateAsync(category);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CategoryDto>(createdCategory);
    }
}
