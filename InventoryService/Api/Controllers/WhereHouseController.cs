using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouses;
using InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseById;
using InventoryService.Application.Featuers.Wherehouse.Queries.SearchByName;
using InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseByCode;
using InventoryService.Application.Featuers.Wherehouse.Commands.CreateWhereHouse;
using InventoryService.Application.Featuers.Wherehouse.Commands.UpdateWhereHouse;
using InventoryService.Application.Featuers.Wherehouse.Commands.RemoveWhereHouse;
using InventoryService.Application.Featuers.Wherehouse.Commands.ActiveWherehouse;
using InventoryService.Application.Featuers.Wherehouse.Commands.DeActiveWhereHouse;

namespace InventoryService.Api.Controllers
{
    [Route("api/[controller]")]
    public class WhereHouseController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetWhereHousesQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetWhereHouseByIdQuery(id), ct);
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
        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode([FromQuery] string code, CancellationToken ct)
        {
            var result = await sender.Send(new GetWhereHouseByCodeQuery(code), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWhereHouse([FromBody] CreateWhereHouseCommand createWhereHouse, CancellationToken ct)
        {
            var result = await sender.Send(createWhereHouse, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWhereHouse(Guid id, [FromBody] UpdateWhereHouseCommand updateWhereHouse, CancellationToken ct)
        {
            if (id != updateWhereHouse.id)
                return BadRequest("Id in URL does not match Id in request body.");
            var result = await sender.Send(updateWhereHouse, ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("BeActive/{id:guid}")]
        public async Task<IActionResult> BeActiveWhereHouse(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new ActiveWherehouseCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpPatch("BeInActive/{id:guid}")]
        public async Task<IActionResult> BeInActiveWhereHouse(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new DeActiveWhereHouseCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWhereHouse(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveWhereHouseCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }

    }
}
