using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Application.Featuers.Brands.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQueryHandler(IAppDbContext context,
        ILogger<GetBrandByIdQueryHandler> logger) : IRequestHandler<GetBrandByIdQuery, Result<BrandDto>>
    {
        public async Task<Result<BrandDto>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await context.brands.Where(b => b.Id == request.id)
                .Select(BrandMapper.ToDto)
                .FirstOrDefaultAsync();
            if (brand is null)
            {
                logger.LogWarning("Brand with id {id} not found", request.id);
                return ProductAppError.NotFound("product", request.id.ToString());
            }
            return brand;
        }
    }
}
