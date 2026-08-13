using System.Linq.Expressions;
using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Domain.Warehouse;

namespace InventoryService.Application.Featuers.Wherehouse.Mapper
{
    public static class WerehouseMapper
    {
        public static readonly Expression<Func<WhereHouse, WherehouseDto>> ToDto=
           w => new WherehouseDto
           {
               Id = w.Id,
               Name = w.Name,
               Code = w.Code,
               Address = w.Address,
               IsActive = w.IsActive
           };
        public static WherehouseDto ToWherehouseDto(this WhereHouse warehouse)
            {
                return new WherehouseDto
                {
                    Id= warehouse.Id,
                    Name = warehouse.Name,
                    Code = warehouse.Code,
                    Address = warehouse.Address,
                    IsActive = warehouse.IsActive
                };
            }
    }
}
