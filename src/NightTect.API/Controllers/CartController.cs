using MediatR;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.Carts.Commands.AddCartItem;
using NightTech.Application.Carts.Commands.UpdateCartItem;
using NightTech.Application.Carts.Querys.GetAll;
using NightTech.Application.Carts.Querys.GetCartsById;
using NightTech.Application.common;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCarts([FromQuery]PaginationParams parameters)
    {
        var result = await mediator.Send(new GetAllCartsQuery(parameters));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCartById(Guid id)
    {
        var result = await mediator.Send(new GetCartByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("by-user")]
    public async Task<IActionResult> GetCartById()
    {
        var id = GetUserCart();
        var result = await mediator.Send(new GetCartByIdQuery(id));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] AddCartItemCommand addCartItems)
    {
        var cartId = GetUserCart();
        addCartItems.CartId = cartId;
        var result = await mediator.Send(addCartItems);
        return CreatedAtAction(nameof(GetCartById), new { id = result.Id }, result);
    }
    [HttpPut("update/{CartItemId:guid}")]
    public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemCommand addCartItems, Guid CartItemId)
    {
        addCartItems.CartId = GetUserCart();
        addCartItems.Id = CartItemId;
        await mediator.Send(addCartItems);
        return NoContent();
    }
    private Guid GetUserCart()
    {
        return Guid.Parse(User.Claims.First(c => c.Type == "CartId").Value);
    }
}
