using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Featuers.Users.Command.CreateSupplier;
using UserService.Application.Featuers.Users.Command.RemoveSupplier;
using UserService.Application.Featuers.Users.Command.UpdateSupplier;
using UserService.Application.Featuers.Users.Queries.GetSupliersById;
using UserService.Application.Featuers.Users.Queries.GetSupplierByCompanyName;
using UserService.Application.Featuers.Users.Queries.GetSupplierByUserId;
using UserService.Application.Featuers.Users.Queries.GetSuppliers;
namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetSupliersQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetSupplierById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetSuplierByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByFullName([FromQuery] string fullName, CancellationToken ct)
        {
            var result = await sender.Send(new GetSupplierByCompanyNameQuery(fullName), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken ct)
        {
            var result = await sender.Send(new GetSupplierByUserIdQuery(userId), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return CreatedAtRoute("GetSupplierById", new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{userid:guid}")]
        public async Task<IActionResult> UpdateSupplier(Guid userid, [FromBody] UpdateSupplierCommand command, CancellationToken ct)
        {
            if (userid != command.userid)
                return BadRequest("User ID in the URL does not match the User ID in the request body.");
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSupplier(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveSupplierCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
    }
}
