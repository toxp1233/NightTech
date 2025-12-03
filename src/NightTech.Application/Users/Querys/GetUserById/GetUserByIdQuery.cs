using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Users.Querys.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;
