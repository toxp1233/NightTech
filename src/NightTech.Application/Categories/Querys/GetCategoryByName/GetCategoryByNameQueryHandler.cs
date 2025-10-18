using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Categories.Querys.GetCategoryByName;

public class GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByNameAsync(request.Name) ?? throw new NotFoundException(nameof(Category), request.Name);
        return mapper.Map<CategoryDto>(category);
    }
}

