using MediatR;
using NightTech.Application.Auth.Dtos;

namespace NightTech.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(Guid UserId, string RefreshToken) : IRequest<TokenDto>;

