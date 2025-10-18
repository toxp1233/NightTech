using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Carts.Querys.GetAll;

public record GetAllCartsQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<CartDto>>;

