
using System.Linq.Expressions;
using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Domain.Stock;

namespace InventoryService.Application.Featuers.stock.Mapper
{
    public static class StockMapper
    {
        public static readonly Expression< Func<Stock, StockDto>> ToDto =
            s => new StockDto
            {
                Id = s.Id,
                ProductId = s.ProductId,
                WarehouseId = s.WarehouseId,
                Quantity = s.Quantity,
            };
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                WarehouseId = stock.WarehouseId,
                Quantity = stock.Quantity,
            };
        }
    }
}
