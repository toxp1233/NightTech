using MediatR;

namespace NightTech.Application.Users.Commands.DisableUser;

public record DisableUserCommand(Guid Id) : IRequest;
