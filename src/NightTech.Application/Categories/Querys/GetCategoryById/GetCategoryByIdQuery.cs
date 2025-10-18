using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Categories.Querys.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>;
