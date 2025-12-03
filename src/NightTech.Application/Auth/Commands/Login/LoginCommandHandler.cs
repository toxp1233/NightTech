using MediatR;
using NightTech.Application.Auth.Dtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtService tokenService,
    IPasswordHasherBcrypt passwordHasherBcrypt,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginCommand, TokenDto>
{
    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User), request.Email);
        var isPasswordValid = passwordHasherBcrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid Password.");
        } else if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("Users account is either banned or deactivated");
        } else if (!user.EmailConfirmed)
        {
            throw new UnauthorizedAccessException("Email is not confirmed. Please confirm your email to login.");
        }
        var tokenDto = new TokenDto
        {
            AccessToken = tokenService.GenerateToken(user),
            RefreshToken = tokenService.GenerateRefreshToken()
        };
        user.RefreshToken = tokenDto.RefreshToken;
        user.RefreshTokenExpireDate = tokenService.GenerateRefreshTokenExpiry();
        await unitOfWork.SaveChangesAsync();
        return tokenDto;
    }
}
