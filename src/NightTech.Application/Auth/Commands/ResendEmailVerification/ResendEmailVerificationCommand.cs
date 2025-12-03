using MediatR;

namespace NightTech.Application.Auth.Commands.ResendEmailVerification;

public record ResendEmailVerificationCommand(string Email) : IRequest<string>;
