using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using SalesService.Domain.Event;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemQuantity
{
    public class UpdateSalesOrderItemQuantityCommandHandler(IAppDbContext context,
        ILogger<UpdateSalesOrderItemQuantityCommandHandler> logger, IMediator mediator) : IRequestHandler<UpdateSalesOrderItemQuantityCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateSalesOrderItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var order = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.salesorderid, cancellationToken);
            if (order is null)
            {
                logger.LogWarning("Sales order with ID {SalesOrderId} not found.", request.salesorderid);
                return AppErrors.NotFound("Sales order", request.salesorderid.ToString());
            }
            var result = order.UpdateItemQuantity(request.productId, request.newQuantity);
            if (result.IsError)
            {
                logger.LogWarning("Failed to update Quantity for item with Product ID {ProductId} in Sales Order ID {SalesOrderId}. Reason: {Reason}",
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
            return result;

        }
    }
}
