using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Featuers.Users.Command.CreateEmployee;
using UserService.Application.Featuers.Users.Command.RemoveEmployee;
using UserService.Application.Featuers.Users.Command.UpdateEmployee;
using UserService.Application.Featuers.Users.Queries.GetEmployeeById;
using UserService.Application.Featuers.Users.Queries.GetEmployeeByUserId;
using UserService.Application.Featuers.Users.Queries.GetEmployees;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetEmployeeQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetEmployeeByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken ct)
        {
            var result = await sender.Send(new GetEmployeeByUserIdQuery(userId), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return CreatedAtRoute("GetEmployeeById", new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{userid:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid userid, [FromBody] UpdateEmployeeCommand command, CancellationToken ct)
        {
            if (userid != command.userid)
                return BadRequest("User ID in the URL does not match the User ID in the request body.");
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveEmployeeCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
    }
}
