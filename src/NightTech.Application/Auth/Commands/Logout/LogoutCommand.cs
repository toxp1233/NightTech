using MediatR;

namespace NightTech.Application.Auth.Commands.Logout;

public record LogoutCommand(Guid Id) : IRequest;
