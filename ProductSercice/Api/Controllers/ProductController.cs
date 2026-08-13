using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Command.ActiveProduct;
using ProductSercice.Application.Featuers.Products.Command.CreateProduct;
using ProductSercice.Application.Featuers.Products.Command.DeActiveProduct;
using ProductSercice.Application.Featuers.Products.Command.RemoveProduct;
using ProductSercice.Application.Featuers.Products.Command.UpdateProduct;
using ProductSercice.Application.Featuers.Products.Queries.GetByBarCode;
using ProductSercice.Application.Featuers.Products.Queries.GetByBrandName;
using ProductSercice.Application.Featuers.Products.Queries.GetByCategoryName;
using ProductSercice.Application.Featuers.Products.Queries.GetProductById;
using ProductSercice.Application.Featuers.Products.Queries.GetProducts;
using ProductSercice.Application.Featuers.Products.Queries.SearchByName;
using ProductSercice.Application.Featuers.Products.Queries.SearchBySKU;
using ProductSercice.Application.Interfaces;

namespace ProductSercice.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController(ISender sender,IAppDbContext context) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetProductsQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetProductById")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetProductByIdQuery(id), ct);
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
        [HttpGet("searchBySKU")]
        public async Task<IActionResult> GetBySKU([FromQuery] string sku, CancellationToken ct)
        {
            var result = await sender.Send(new SearchBySKUQuery(sku), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("searchByBarcode")]
        public async Task<IActionResult> GetByBarcode([FromQuery] string barcode, CancellationToken ct)
        {
            var result = await sender.Send(new GetByBarCodeQuery(barcode), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("GetByCategoryName")]
        public async Task<IActionResult> GetByCategoryName([FromQuery] string categoryName, CancellationToken ct)
        {
            var result = await sender.Send(new GetByCategoryNameQuery(categoryName), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("GetByBrandName")]
        public async Task<IActionResult> GetByCategoryBrand([FromQuery] string brandname, CancellationToken ct)
        {
            var result = await sender.Send(new GetByBrandNameQuery(brandname), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProducrCommand createProduct, CancellationToken ct)
        {
            var result = await sender.Send(createProduct, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand updateProduct, CancellationToken ct)
        {
            if (id != updateProduct.id)
                return BadRequest("Id in URL does not match Id in request body.");

            var result = await sender.Send(updateProduct, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveProductCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> BeIctive(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new ActiveProductCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> BeDeactive(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new DeActiveProductCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpGet("internal/{id:guid}")]
        public async Task<IActionResult> GetProductByIdInternal(Guid id, CancellationToken ct)
        {
            var product=await context.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Status
                }).FirstOrDefaultAsync(ct);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
