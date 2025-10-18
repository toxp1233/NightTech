using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.common;
using NightTech.Application.Orders.Commands.CreateOrder;
using NightTech.Application.Orders.Commands.UpdateOrder;
using NightTech.Application.Orders.Querys.GetAllOrder;
using NightTech.Application.Orders.Querys.GetOrderById;
using NightTech.Application.Orders.Querys.GetUsersOrder;
using System.Security.Claims;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpGet("All")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAllOrders([FromQuery] PaginationParams paginationParams)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(paginationParams));
        return Ok(result);
    }
    [HttpGet("{Id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetOrderById(Guid Id)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(Id));
        return Ok(result);
    }

    [HttpPost("by-user-id")]
    public async Task<IActionResult> GetOrderByUserId()
    {
        var userId = GetUserId();
        var result = await mediator.Send(new GetUsersOrderQuery(userId));
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder()
    {
        var userid = GetUserId();
        var cartid = GetUserCart();
        var result = await mediator.Send(new CreateOrderCommand(userid, cartid));
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateOrder(UpdateOrderCommand updateOrderCommand)
    {
        var result = await mediator.Send(updateOrderCommand);
        return Ok(result);
    }

    private Guid GetUserId()
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(claim))
            throw new Exception("User ID not found in token claims.");
        return Guid.Parse(claim);
    }

    private Guid GetUserCart()
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == "CartId")?.Value;
        if (string.IsNullOrEmpty(claim))
            throw new Exception("Cart ID not found in token claims.");
        return Guid.Parse(claim);
    }

}
