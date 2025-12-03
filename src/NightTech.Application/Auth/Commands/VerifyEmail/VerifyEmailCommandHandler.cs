using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;
using NightTech.Application.common;
namespace NightTech.Application.Auth.Commands.VerifyEmail;

public class VerifyEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtService jwtService
) : IRequestHandler<VerifyEmailCommand, string>
{
    public async Task<string> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = jwtService.ValidateEmailVerificationToken(request.Token);

        if (userId == null)
            return "User Id Didnt came back";

        var user = await userRepository.GetByGuidAsync(userId.Value)
            ?? throw new NotFoundException(nameof(User), userId.Value.ToString());

        if (user.EmailConfirmed)
            return "Email already verified.";

        user.EmailConfirmed = true;
        await unitOfWork.SaveChangesAsync();

        return "Email verified successfully!";
    }
}
