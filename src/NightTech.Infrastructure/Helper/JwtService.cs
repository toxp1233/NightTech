using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NightTech.Domain.Entities;
using NightTech.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NightTech.Infrastructure.Helper;

public class JwtService(IConfiguration config) : IJwtService
{
    private readonly IConfiguration _config = config;

    public string GenerateToken(User user)
    {
        var jwtKey = _config["TokenSettings:Key"];
        var issuer = _config["TokenSettings:Issuer"];
        var audience = _config["TokenSettings:Audience"];

        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("TokenSettings: Key not found in configuration");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
            new Claim("Country", user.Country ?? string.Empty),
            new Claim("IsAdmin", user.IsAdmin.ToString()),
            new Claim("FirstName", user.FirstName ?? string.Empty),
            new Claim("LastName", user.LastName ?? string.Empty),
            new Claim("CreatedAt", user.CreatedAt.ToString()),
            new Claim("CartId", user.CartId?.ToString() ?? string.Empty),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public DateTime GenerateRefreshTokenExpiry()
    {
        return DateTime.UtcNow.AddDays(7);
    }

    public string ValidateRefreshToken(string refreshToken, User user)
    {
        if (user.RefreshToken != refreshToken || user.RefreshTokenExpireDate < DateTime.UtcNow || user == null)
        {
            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        return GenerateToken(user);
    }
}
