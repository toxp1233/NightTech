using MediatR;

namespace NightTech.Application.Users.Commands.CreateAccount;

public record CreateAccountCommand(
    string UserName,
    string Email,
    string Password,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Country
 ) : IRequest<string>;

