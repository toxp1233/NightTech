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
