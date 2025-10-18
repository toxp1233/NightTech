using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.common;
using NightTech.Application.Products.Commands.AddProduct;
using NightTech.Application.Products.Commands.DeleteProduct;
using NightTech.Application.Products.Commands.UpdateProduct;
using NightTech.Application.Products.Querys.GetAllProducts;
using NightTech.Application.Products.Querys.GetProductsById;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParams paginationParams)
    {
        var result = await mediator.Send(new GetAllProductsQuery(paginationParams));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdProduct(Guid id)
    {
        var result = await mediator.Send(new GetProductsByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand request)
    {
        var result = await mediator.Send(request);
        return CreatedAtAction(nameof(GetByIdProduct), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand request)
    {
        await mediator.Send(request);
        return NoContent();
    }
}
