using InventoryService.Application.Featuers.stock.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStocks
{
    public sealed record GetStocksQuery : IRequest<Result<List<StockDto>>>
    {
    }
}
