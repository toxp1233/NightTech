using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.Auth.Commands.Login;
using NightTech.Application.Auth.Commands.Logout;
using NightTech.Application.Auth.Commands.RefreshToken;
using NightTech.Application.Auth.Commands.Register;

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
    public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenCommand refreshTokenCommand)
    {
        var result = await mediator.Send(refreshTokenCommand);
        return Ok(result);
    }
}
