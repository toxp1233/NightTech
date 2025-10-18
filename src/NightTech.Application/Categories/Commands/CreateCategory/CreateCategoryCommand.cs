using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string CategoryName) : IRequest<CategoryDto>;
