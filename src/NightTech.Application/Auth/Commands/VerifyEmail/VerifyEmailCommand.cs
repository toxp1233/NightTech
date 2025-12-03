using MediatR;

namespace NightTech.Application.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(string Token) : IRequest<string>;
