using MediatR;
using NightTech.Application.Auth.Dtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUserRepository userRepository, IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByGuidAsync(request.UserId) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        var token = jwtService.ValidateRefreshToken(request.RefreshToken, user);
        return new TokenDto
        {
           AccessToken = token,
           RefreshToken = request.RefreshToken
        };
    }
}
