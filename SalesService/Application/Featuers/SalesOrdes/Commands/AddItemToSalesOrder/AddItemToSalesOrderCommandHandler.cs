using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;
using SalesService.Application.Interfaces;
using SalesService.Domain.Event;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder
{
    public class AddItemToSalesOrderCommandHandler(IAppDbContext context,
        ILogger<AddItemToSalesOrderCommandHandler> logger, IProductServiceClient product, IMediator mediator) : IRequestHandler<AddItemToSalesOrderCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(AddItemToSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var productExists = await product.GetProductByIdAsync(request.productId, cancellationToken);
            if (productExists == null)
            {
                logger.LogWarning("Product with ID {ProductId} does not exist.", request.productId);
                return AppErrors.NotFound("product", request.productId.ToString());
            }

            var salesOrder = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.salesorderid,cancellationToken);
            if (salesOrder == null)
            {
                logger.LogWarning("Sales order with ID {SalesOrderId} does not exist.", request.salesorderid);
                return AppErrors.NotFound("salesorder", request.salesorderid.ToString());
            }
            var result = salesOrder.AddItem(request.productId, request.quantity, request.unitPrice, request.discount, request.tax);
            if (result.IsError)
            {
                logger.LogWarning("Failed to add item to sales order: {Errors}", result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            var item = salesOrder.Items.First(
           x => x.ProductId == request.productId);
            var salesEvent = new SalesEvent(
            eventId: Guid.NewGuid(),
            salesOrderId: salesOrder.Id,
            orderNumber: salesOrder.OrderNumber,
            productId: item.ProductId,
            quantity: item.Quantity,
            unitPrice: item.UnitPrice,
            discount: item.Discount,
            tax: item.Tax,
            total: item.Total);
            await mediator.Publish(salesEvent, cancellationToken);

            return result;
        }
    }
}
