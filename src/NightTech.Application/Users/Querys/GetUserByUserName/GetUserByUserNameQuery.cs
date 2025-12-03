using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Users.Querys.GetUserByUserName;

public record GetUserByUserNameQuery(string UserName) : IRequest<UserDto?>;
