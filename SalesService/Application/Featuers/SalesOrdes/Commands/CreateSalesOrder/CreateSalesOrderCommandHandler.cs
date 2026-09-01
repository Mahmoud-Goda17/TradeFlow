using MediatR;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;
using SalesService.Application.Interfaces;
using SalesService.Domain.SalesOrders;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.CreateSalesOrder 
{
    public class CreateSalesOrderCommandHandler(IAppDbContext context,
        ILogger<CreateSalesOrderCommandHandler> logger, IProductServiceClient product, ICustomerServiceClient customer) : IRequestHandler<CreateSalesOrderCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {

            var customerExists = await customer.GetCustomerByIdAsync(request.customerId, cancellationToken);
            if (customerExists == null)
            {
                logger.LogWarning("customer with ID {customerId} does not exist.", request.customerId);
                return AppErrors.NotFound("customer", request.customerId.ToString());
            }

            var orderResult = SalesOrder.Create(request.orderNumber, request.customerId, request.shippingAddress);
            if (orderResult.IsError)
            {
                logger.LogWarning("Failed to create sales order: {Errors}", orderResult.Errors);
                return orderResult.Errors;
            }
            var order = orderResult.Value;
            foreach (var item in request.items)
            {
                var productExists = await product.GetProductByIdAsync(item.productId, cancellationToken);
                if (productExists == null)
                {
                    logger.LogWarning("Product with ID {ProductId} does not exist.", item.productId);
                    return AppErrors.NotFound("product", item.productId.ToString());
                }

                var addItemResult = order.AddItem(item.productId, item.quantity, item.unitPrice, item.discount, item.tax);
                if (addItemResult.IsError)
                {
                    logger.LogWarning("Failed to add item to sales order: {Errors}", addItemResult.Errors);
                    return addItemResult.Errors;
                }
            }

            context.Sales.Add(order);
            await context.SaveChangesAsync(cancellationToken);
            return order.Id;
        }
    }
}
