using NightTech.Domain.Entities;

namespace NightTech.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    DateTime GenerateRefreshTokenExpiry();
    string ValidateRefreshToken(string refreshToken, User user);
}
