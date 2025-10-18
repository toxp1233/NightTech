using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NightTech.Application.Categories.Commands.CreateCategory;
using NightTech.Application.Categories.Commands.DeleteCategory;
using NightTech.Application.Categories.Commands.UpdateCategory;
using NightTech.Application.Categories.Querys.GetAllCategories;
using NightTech.Application.Categories.Querys.GetCategoryById;
using NightTech.Application.Categories.Querys.GetCategoryByName;

namespace NightTect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{name}")]   
    public async Task<IActionResult> GetCategoryByName(string name)
    {
        var result = await mediator.Send(new GetCategoryByNameQuery(name));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand createCategoryCommand)
    {
        var result = await mediator.Send(createCategoryCommand);
        return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
    }

    [HttpPut("update/{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand updateCategoryCommand)
    {
        updateCategoryCommand.Id = id;
        await mediator.Send(updateCategoryCommand);
        return NoContent();
    }


    [HttpDelete]
    [Route("delete/{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteCategory(int id, DeleteCategoryCommand deleteCategoryCommand)
    {
        deleteCategoryCommand.Id = id;
        var result = await mediator.Send(deleteCategoryCommand);
        return Ok(result);
    }
}
