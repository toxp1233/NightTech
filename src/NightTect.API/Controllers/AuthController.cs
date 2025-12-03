using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.Auth.Commands.Login;
using NightTech.Application.Auth.Commands.Logout;
using NightTech.Application.Auth.Commands.RefreshToken;
using NightTech.Application.Auth.Commands.Register;
using NightTech.Application.Auth.Commands.ResendEmailVerification;
using NightTech.Application.Auth.Commands.VerifyEmail;
using System.Net;
using System.Text;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var result = await mediator.Send(registerCommand);
        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var result = await mediator.Send(loginCommand);
        return Ok(result);
    }
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand logoutCommand)
    {
        await mediator.Send(logoutCommand);
        return NoContent();
    }

    [HttpPost("Refresh-Token")]
    [Authorize]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
    {
        var result = await mediator.Send(refreshTokenCommand);
        return Ok(result);
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        var result = await mediator.Send(new VerifyEmailCommand(token));
        return Ok(result);
    }

    [HttpPost("resend-email-verification")]
    public async Task<IActionResult> ResendEmailVerification([FromBody] string email)
    {
        var result = await mediator.Send(new ResendEmailVerificationCommand(email));
        return Ok(result);
    }

}
