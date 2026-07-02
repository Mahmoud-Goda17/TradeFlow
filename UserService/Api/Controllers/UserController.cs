using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Featuers.Users.Command.ActiveUser;
using UserService.Application.Featuers.Users.Command.CreateUser;
using UserService.Application.Featuers.Users.Command.NotActiveUser;
using UserService.Application.Featuers.Users.Command.RemoveUser;
using UserService.Application.Featuers.Users.Command.UpdateUser;
using UserService.Application.Featuers.Users.Queries.GetUserByFullName;
using UserService.Application.Featuers.Users.Queries.GetUserById;
using UserService.Application.Featuers.Users.Queries.GetUsers;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetUsersQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetUserByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByFullName([FromQuery] string fullName, CancellationToken ct)
        {
            var result = await sender.Send(new GetUserByFullNameQuery(fullName), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUser, CancellationToken ct)
        {
            var result = await sender.Send(createUser, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand updateUser, CancellationToken ct)
        {
            if (id != updateUser.id)
                return BadRequest("Id in URL does not match Id in request body.");
            var result = await sender.Send(updateUser, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveUserCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> BeIctive(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new ActiveUserCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> BeDeactive(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new NotActiveUserCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
    }
}
