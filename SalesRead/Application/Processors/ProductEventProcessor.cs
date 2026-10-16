using Microsoft.EntityFrameworkCore;
using SalesRead.Application.Interfaces;
using SalesRead.Application.SalesOrderProductReadModels.Dtos;

namespace SalesRead.Application.Processors
{
    public sealed class ProductEventProcessor(IAppDbContext dbContext)
    {
        public async Task ProcessAsync(ProductEventDto productEvent,CancellationToken cancellationToken)
        {
            var readModels =
                await dbContext.SalesOrderProductReadModels
                    .Where(x => x.ProductId == productEvent.ProductId)
                    .ToListAsync(cancellationToken);

            foreach (var readModel in readModels)
            {
                readModel.Update(
                    readModel.OrderNumber,
                    productEvent.ProductName,
                    readModel.Quantity,
                    readModel.UnitPrice,
                    readModel.Discount,
                    readModel.Tax,
                    readModel.Total);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
