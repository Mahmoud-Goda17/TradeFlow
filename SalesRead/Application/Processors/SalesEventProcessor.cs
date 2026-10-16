using Microsoft.EntityFrameworkCore;
using SalesRead.Application.Interfaces;
using SalesRead.Application.SalesOrderProductReadModels.Dtos;
using SalesRead.Damain.SalesOrderProductReadModels;

namespace SalesRead.Application.Processors
{
    public sealed class SalesEventProcessor(IAppDbContext dbContext)
    {
        public async Task ProcessAsync(
            SalesEventDto salesEvent,
            CancellationToken cancellationToken)
        {
            var readModel =
                await dbContext.SalesOrderProductReadModels
                    .FirstOrDefaultAsync(
                        x => x.SalesOrderId == salesEvent.SalesOrderId
                          && x.ProductId == salesEvent.ProductId,
                        cancellationToken);

            if (readModel is null)
            {
                readModel = new SalesOrderProductReadModel(
                    salesEvent.SalesOrderId,
                    salesEvent.OrderNumber,
                    salesEvent.ProductId,
                    string.Empty,
                    salesEvent.Quantity,
                    salesEvent.UnitPrice,
                    salesEvent.Discount,
                    salesEvent.Tax,
                    salesEvent.Total);

                dbContext.SalesOrderProductReadModels.Add(readModel);
            }
            else
            {
                readModel.Update(
                    salesEvent.OrderNumber,
                    readModel.ProductName,
                    salesEvent.Quantity,
                    salesEvent.UnitPrice,
                    salesEvent.Discount,
                    salesEvent.Tax,
                    salesEvent.Total);
            }
        }
    }
}
