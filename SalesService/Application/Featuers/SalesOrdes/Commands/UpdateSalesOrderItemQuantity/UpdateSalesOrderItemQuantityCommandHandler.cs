using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemQuantity
{
    public class UpdateSalesOrderItemQuantityCommandHandler(IAppDbContext context,
        ILogger<UpdateSalesOrderItemQuantityCommandHandler> logger) : IRequestHandler<UpdateSalesOrderItemQuantityCommand, Result<Updated>>
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
            return result;

        }
    }
}
