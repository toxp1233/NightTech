using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.common;
using NightTech.Application.Users.Commands.AssignRole;
using NightTech.Application.Users.Commands.CreateAccount;
using NightTech.Application.Users.Commands.DisableUser;
using NightTech.Application.Users.Querys.GetAllUsers;
using NightTech.Application.Users.Querys.GetUserByEmail;
using NightTech.Application.Users.Querys.GetUserById;
using NightTech.Application.Users.Querys.GetUserByUserName;
using NightTech.Domain.Constants;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginationParams paginationParams)
    {
        var result = await mediator.Send(new GetAllUsersQuery(paginationParams));
        return Ok(result);
    }

    [HttpGet("ById/{Id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserById(Guid Id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(Id));
        return Ok(result);
    }

    [HttpGet("ByEmail/{Email}")]
    public async Task<IActionResult> GetUserByEmail(string Email)
    {
        var result = await mediator.Send(new GetUserByEmailQuery(Email));
        return Ok(result);
    }

    [HttpGet("ByUserName/{UserName}")]
    public async Task<IActionResult> GetUserByUserName(string UserName)
    {
        var result = await mediator.Send(new GetUserByUserNameQuery(UserName));
        return Ok(result);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRoleToUser(Guid UserId, UserRole Role)
    {
        await mediator.Send(new AssignRoleCommand(UserId, Role));
        return NoContent();
    }

    [HttpPost("DisableUser/{UserId:guid}")]
    public async Task<IActionResult> DisableUser(Guid UserId)
    {
        await mediator.Send(new DisableUserCommand(UserId));
        return NoContent();
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateAccount(CreateAccountCommand createAccount)
    {
        var result = await mediator.Send(createAccount);
        return Ok(result);
    }
}
