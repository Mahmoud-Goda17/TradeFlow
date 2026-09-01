using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Featuers.Users.Command.CreateCustomer;
using UserService.Application.Featuers.Users.Command.RemoveCustomer;
using UserService.Application.Featuers.Users.Command.UpdateCustomer;
using UserService.Application.Featuers.Users.Queries.GetCustomerBuUserId;
using UserService.Application.Featuers.Users.Queries.GetCustomerByCompanyName;
using UserService.Application.Featuers.Users.Queries.GetCustomerById;
using UserService.Application.Featuers.Users.Queries.GetCustomers;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController(ISender sender,IAppDbContext context) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetCustomerQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetCutomerByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByFullName([FromQuery] string fullName, CancellationToken ct)
        {
            var result = await sender.Send(new GetCustomerByCompanyNameQuery(fullName), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken ct)
        {
            var result = await sender.Send(new GetCustomerByUserIdQuery(userId), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return CreatedAtRoute("GetCustomerById", new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{userid:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid userid, [FromBody] UpdateCustomerCommand command, CancellationToken ct)
        {
            if (userid != command.usreid)
                return BadRequest("User ID in the URL does not match the User ID in the request body.");
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveCustomerCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpGet("internal/{id:guid}")]
        public async Task<IActionResult> GetProductByIdInternal(Guid id, CancellationToken ct)
        {
            var customer = await context.Customers
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                }).FirstOrDefaultAsync(ct);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

    }
}
