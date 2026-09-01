using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder;
using SalesService.Application.Featuers.SalesOrdes.Commands.CreateSalesOrder;
using SalesService.Application.Featuers.SalesOrdes.Commands.RemoveSalesOrderItem;
using SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemDiscount;
using SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemQuantity;
using SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderById;
using SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderByOrderNumber;
using SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrders;

namespace SalesService.Api.Controllers
{
    [Route("api/[controller]")]
    public class SalesOrderController(ISender sender):ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(GetSalesOrdersQuery ordersQuery,CancellationToken token)
        {
            var result = await sender.Send(ordersQuery,token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken token)
        {
            var result = await sender.Send(new GetSalesOrderByIdQuery(id), token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpGet("GetByOrderNumber")]
        public async Task<IActionResult> GetByProductId(string productId, CancellationToken token)
        {
            var result = await sender.Send(new GetSalesOrderByOrderNumberQuery( productId), token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSalesOrder([FromBody] CreateSalesOrderCommand createSalesOrder, CancellationToken token)
        {
            var result = await sender.Send(createSalesOrder, token);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetById), new { id = result.Value }, result);
            return Problem(result.Errors);
        }
        [HttpPost("AddItemToSalesOrder")]
        public async Task<IActionResult> AddItemToSalesOrder([FromBody] AddItemToSalesOrderCommand addItemToSalesOrder, CancellationToken token)
        {
            var result = await sender.Send(addItemToSalesOrder, token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSalesOrder([FromBody] RemoveSalesOrderItemCommand deleteSalesOrder, CancellationToken token)
        {
            var result = await sender.Send(deleteSalesOrder, token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("UpdateSalesOrderItemDiscount")]
        public async Task<IActionResult> UpdateSalesOrderItemDiscount([FromBody] UpdateSalesOrderItemDiscountCommand UpdateSalesOrderItemDiscount, CancellationToken token)
        {
            var result = await sender.Send(UpdateSalesOrderItemDiscount, token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
        [HttpPatch("UpdateSalesOrderItemQuantity")]
        public async Task<IActionResult> UpdateSalesOrderItemQuantity([FromBody] UpdateSalesOrderItemQuantityCommand UpdateSalesOrderItemQuantity, CancellationToken token)
        {
            var result = await sender.Send(UpdateSalesOrderItemQuantity, token);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }

    }

}
