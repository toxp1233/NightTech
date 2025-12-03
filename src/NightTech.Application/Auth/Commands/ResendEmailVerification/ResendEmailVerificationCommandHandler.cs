using MediatR;
using NightTech.Application.common;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Auth.Commands.ResendEmailVerification;

public class ResendEmailVerificationCommandHandler(
    IEmailService emailService,
    IUserRepository userRepository,
    IJwtService jwtService
    ) : IRequestHandler<ResendEmailVerificationCommand, string>
{
    public async Task<string> Handle(ResendEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User), request.Email);
        if(user.EmailConfirmed)
        {
            return "This Users Email was confirmed";
        } else if (!user.IsActive) {
            return "this user is banned";
        }
        var token = jwtService.GenerateEmailVerificationToken(user.Id);
        var verifyLink = $"https://localhost:7225/api/auth/verify-email?token={token}";
        await emailService.SendEmailAsync(user.Email, verifyLink);
        return $"Verification link was sent to {user.Email}";
    }
}
