using MediatR;
using NightTech.Application.Auth.Dtos;

namespace NightTech.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<TokenDto>;
