using MediatR;
using NightTech.Application.Auth.Dtos;

namespace NightTech.Application.Auth.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Country
    ) : IRequest<TokenDto>;
