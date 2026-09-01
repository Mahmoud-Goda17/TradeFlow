using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.RemoveSalesOrderItem
{
    public class RemoveSalesOrderItemCommandHandler(IAppDbContext context,
        ILogger<RemoveSalesOrderItemCommandHandler> logger) : IRequestHandler<RemoveSalesOrderItemCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(RemoveSalesOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await context.Sales.FirstOrDefaultAsync(x => x.Id == request.salesorderid,cancellationToken);
            if (order is null)
            {
                logger.LogWarning("Sales order with ID {SalesOrderId} not found.", request.salesorderid);
                return AppErrors.NotFound("Sales order", request.salesorderid.ToString());
            }
            var result = order.RemoveItem(request.productId);
            if (result.IsError)
            {
                logger.LogWarning("Failed to remove item with Product ID {ProductId} from Sales Order ID {SalesOrderId}. Reason: {Reason}",
                    request.productId, request.salesorderid, result.Errors);
                return result.Errors;
            }
           await context.SaveChangesAsync(cancellationToken);
           return result ;
        }
    }
}
