using InventoryService.Application.Featuers.stock.Commands.ConfirmReservationStock;
using InventoryService.Application.Featuers.stock.Commands.CreateStock;
using InventoryService.Application.Featuers.stock.Commands.DecreaseStock;
using InventoryService.Application.Featuers.stock.Commands.IncreaseStock;
using InventoryService.Application.Featuers.stock.Commands.ReleasesStock;
using InventoryService.Application.Featuers.stock.Commands.RemoveStock;
using InventoryService.Application.Featuers.stock.Commands.ReserveStock;
using InventoryService.Application.Featuers.stock.Commands.UpdateStock;
using InventoryService.Application.Featuers.stock.Queries.GetStockById;
using InventoryService.Application.Featuers.stock.Queries.GetStockByProductId;
using InventoryService.Application.Featuers.stock.Queries.GetStocks;
using InventoryService.Application.Featuers.stock.Queries.GetStocksByWhereHouseId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
    [Route("api/[controller]")]
    public class StockController(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetStocksQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByid(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetStockByIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("GetByProductId/{id:guid}")]
        public async Task<IActionResult> GetByProductId(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetStockByProductIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("GetByWhereHouseId/{id:guid}")]
        public async Task<IActionResult> GetByWhereHouseId(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetStocksByWhereHouseIdQuery(id), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockCommand createStock, CancellationToken ct)
        {
            var result = await sender.Send(createStock, ct);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetByid), new { id = result.Value.Id }, result);
            return Problem(result.Errors);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockCommand updateStock, CancellationToken ct)
        {
            if (id != updateStock.id)
            {
                return BadRequest("Id in URL does not match Id in body");
            }
            var result = await sender.Send(updateStock, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("IncreaseStock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("DecreaseStock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("ReleaseStock")]
        public async Task<IActionResult> ReleaseStock([FromBody] ReleaseStockCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("ReserveStock")]
        public async Task<IActionResult> ReserveStock([FromBody] ReserveStockCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("ConfirmReservationStock")]
        public async Task<IActionResult> ConfirmReservationStock([FromBody] ConfirmReservationStockCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStock(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveStockCommand(id), ct);
            if (result.IsSuccess)
                return NoContent();
            return Problem(result.Errors);
        }
        

    }
}
