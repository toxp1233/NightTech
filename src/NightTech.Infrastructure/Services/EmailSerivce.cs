using Microsoft.Extensions.Configuration;
using MimeKit;
using NightTech.Domain.Interfaces;
namespace NightTech.Infrastructure.Services;

public class EmailSerivce(IConfiguration config) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string verificationLink)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(config["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = "Verify Your Email";
        email.Body = new TextPart("html")
        {
            Text = $"<p>Welcome to NightTech!</p><p>Click the link below to verify your email:</p>" +
                   $"<a href='{verificationLink}'>Verify Email</a>"
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync(config["EmailSettings:SmtpServer"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(config["EmailSettings:From"], config["EmailSettings:Password"]);
        await smtp.SendAsync(email, CancellationToken.None);
        await smtp.DisconnectAsync(true);
    }
}
