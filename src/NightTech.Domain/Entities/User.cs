using NightTech.Domain.Constants;

namespace NightTech.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; } = false;
    public UserRole Role { get; set; } = UserRole.User;
    public string? PhoneNumber { get; set; }
    public string? Country { get; set;}
    public virtual ICollection<Order> Orders { get; set; } = [];
    public virtual Guid? CartId { get; set; }
    public virtual Cart? Cart { get; set; }
}
