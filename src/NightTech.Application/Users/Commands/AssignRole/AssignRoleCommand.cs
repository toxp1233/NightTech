using MediatR;
using NightTech.Domain.Constants;

namespace NightTech.Application.Users.Commands.AssignRole;

public record AssignRoleCommand(Guid Id, UserRole Role) : IRequest;