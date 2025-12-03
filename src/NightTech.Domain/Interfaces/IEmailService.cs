namespace NightTech.Domain.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toMail, string verificationLink);
}
