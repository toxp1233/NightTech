using MediatR;
using NightTech.Application.PublicDtos;

namespace NightTech.Application.Users.Querys.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;
