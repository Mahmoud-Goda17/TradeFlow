using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Application.Featuers.Wherehouse.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.SearchByName
{
    public class SearchByNameQueryHandler(IAppDbContext context) : IRequestHandler<SearchByNameQuery, Result<List<WherehouseDto>>>
    {
        public async Task<Result<List<WherehouseDto>>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            var searchname = request.name.Trim();
            var wherehouses = await context.Whouses.Where(p => p.Name.Contains(searchname))
                .Select(WerehouseMapper.ToDto)
                .ToListAsync();
            return wherehouses;
        }
    }
}
