using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductSercice.Application.Featuers.Categories.Commands.CreateCategory;
using ProductSercice.Application.Featuers.Categories.Commands.RemoveCategory;
using ProductSercice.Application.Featuers.Categories.Commands.UpdateCategory;
using ProductSercice.Application.Featuers.Categories.Queries.GetCategories;
using ProductSercice.Application.Featuers.Categories.Queries.GetCategoryById;
using ProductSercice.Application.Featuers.Categories.Queries.SearchByName;

namespace ProductSercice.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetCategoriesQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("searchByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name, CancellationToken ct)
        {
            var result = await sender.Send(new SearchByNameQuery(name), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand createCategory, CancellationToken ct)
        {
            var result = await sender.Send(createCategory, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id,[FromBody] UpdateCategoryCommand updateCategory, CancellationToken ct)
        {
            if (id != updateCategory.id)
                return BadRequest("Id in URL does not match Id in request body.");

            var result = await sender.Send(updateCategory, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveCategoryCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }

    }
}
