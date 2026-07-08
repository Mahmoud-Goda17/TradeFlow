using System.Linq.Expressions;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Domain.Brand;

namespace ProductSercice.Application.Featuers.Brands.Mapper
{
    public static class BrandMapper
    {
        public static readonly Expression<Func<Brand, BrandDto>> ToDto =
            b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Country = b.Country
            };
        public static BrandDto ToBrandDto(this Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                Country = brand.Country
            };
        }
    }
}
