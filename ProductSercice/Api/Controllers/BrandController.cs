using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductSercice.Application.Featuers.Brands.Commands.CreateBrand;
using ProductSercice.Application.Featuers.Brands.Commands.RemoveBrand;
using ProductSercice.Application.Featuers.Brands.Commands.UpdateBrand;
using ProductSercice.Application.Featuers.Brands.Queries.GetBrandById;
using ProductSercice.Application.Featuers.Brands.Queries.GetBrands;
using ProductSercice.Application.Featuers.Brands.Queries.SearchByName;

namespace ProductSercice.Api.Controllers
{
    [Route("api/[controller]")]
    public class BrandController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetBrandsQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetBrandById")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetBrandByIdQuery(id), ct);
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
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand createBrand, CancellationToken ct)
        {
            var result = await sender.Send(createBrand, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] UpdateBrandCommand updateBrand, CancellationToken ct)
        {
            if (id != updateBrand.id)
                return BadRequest("Id in URL does not match Id in request body.");
            var result = await sender.Send(updateBrand, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBrand(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveBrandCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
    }
}
