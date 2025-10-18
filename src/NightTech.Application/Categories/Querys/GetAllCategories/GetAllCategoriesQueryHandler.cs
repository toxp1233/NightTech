using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Categories.Querys.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository, 
    IMapper mapper
    ) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();
        return mapper.Map<List<CategoryDto>>(categories);
    }
}
