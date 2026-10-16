using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using SalesService.Domain.Event;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemDiscount
{
    public class UpdateSalesOrderItemDiscountCommandHandler(IAppDbContext context,
        ILogger<UpdateSalesOrderItemDiscountCommandHandler> logger, IMediator mediator) : IRequestHandler<UpdateSalesOrderItemDiscountCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateSalesOrderItemDiscountCommand request, CancellationToken cancellationToken)
        {
            var order = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.salesorderid,cancellationToken);
            if (order is null)
            {
                logger.LogWarning("Sales order with ID {SalesOrderId} not found.", request.salesorderid);
                return AppErrors.NotFound("Sales order", request.salesorderid.ToString());
            }
            var result = order.UpdateItemDiscount(request.productId, request.newDiscount);
            if (result.IsError)
            {
                logger.LogWarning("Failed to update discount for item with Product ID {ProductId} in Sales Order ID {SalesOrderId}. Reason: {Reason}",
                    request.productId, request.salesorderid, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            var item = order.Items.First(x => x.ProductId == request.productId);

            var salesEvent = new SalesEvent(
                eventId: Guid.NewGuid(),
                salesOrderId: order.Id,
                orderNumber: order.OrderNumber,
                productId: item.ProductId,
                quantity: item.Quantity,
                unitPrice: item.UnitPrice,
                discount: item.Discount,
                tax: item.Tax,
                total: item.Total
            );

            await mediator.Publish(salesEvent, cancellationToken);
            return result;
        }
    }
}
