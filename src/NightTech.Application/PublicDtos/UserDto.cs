using NightTech.Domain.Constants;

namespace NightTech.Application.PublicDtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Country { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
