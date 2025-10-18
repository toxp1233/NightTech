using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Categories.Querys.GetCategoryByName;

public record GetCategoryByNameQuery(string Name) : IRequest<CategoryDto>;

