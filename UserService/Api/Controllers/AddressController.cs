using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Featuers.Users.Command.CreateAddress;
using UserService.Application.Featuers.Users.Command.RemoveAddress;
using UserService.Application.Featuers.Users.Command.UpdateAddress;
using UserService.Application.Featuers.Users.Queries.GetAddressByUserId;
namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController(ISender sender) : ApiController
    {
        [HttpGet("user/{userId:guid}",Name = "GetAddressesByuserId")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken ct)
        {
            var result = await sender.Send(new GetAddressByUserIdQuery(userId), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return CreatedAtRoute("GetAddressesByuserId", new { id = command.userid }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{userid:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid userid, [FromBody] UpdateAddressCommand command, CancellationToken ct)
        {
            if (userid != command.userid)
                return BadRequest("User ID in the URL does not match the User ID in the request body.");
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAddress(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveAddressCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
    }
}
