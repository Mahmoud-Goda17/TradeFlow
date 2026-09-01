using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder
{
    public class AddItemToSalesOrderCommandHandler(IAppDbContext context,
        ILogger<AddItemToSalesOrderCommandHandler> logger, IProductServiceClient product) : IRequestHandler<AddItemToSalesOrderCommand, Result<Updated>>
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
            return result;
        }
    }
}
