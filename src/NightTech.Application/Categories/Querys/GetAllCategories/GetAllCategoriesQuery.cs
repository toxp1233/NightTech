using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Categories.Querys.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;
