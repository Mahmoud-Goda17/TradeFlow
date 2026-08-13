using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Application.Featuers.stock.Queries.GetStocks
{
    public class GetStocksQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetStocksQuery, Result<List<StockDto>>>
    {
        public async Task<Result<List<StockDto>>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
        {
            var stocksCache = await cache.GetOrCreateAsync("stocks", entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var stocks = context.Stocks.Select(StockMapper.ToDto)
                    .ToListAsync(cancellationToken);
                return stocks;
            });
            return stocksCache;
        }
    }
}
