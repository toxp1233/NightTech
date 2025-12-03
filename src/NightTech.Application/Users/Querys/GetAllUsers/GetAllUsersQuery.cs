using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Users.Querys.GetAllUsers;

public record GetAllUsersQuery(PaginationParams PaginationParams) : IRequest<PaginatedList<UserDto>>;
