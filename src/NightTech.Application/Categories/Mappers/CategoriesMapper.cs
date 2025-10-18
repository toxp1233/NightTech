using AutoMapper;
using NightTech.Application.Categories.Commands.CreateCategory;
using NightTech.Application.Categories.Commands.UpdateCategory;
using NightTech.Domain.Entities;

namespace NightTech.Application.Categories.Mappers;

public class CategoriesMapper : Profile
{
    public CategoriesMapper()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
    }
}
