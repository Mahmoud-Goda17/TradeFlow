using InventoryService.Domain.Stock;
using InventoryService.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.InfraStrucure.Data.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductId)
                .IsRequired();
            builder.Property(x => x.WarehouseId)
                .IsRequired();
            builder.Property(x => x.Quantity)
                .IsRequired();
            builder.Property(x => x.ReservedQuantity)
                .IsRequired();
            builder.HasIndex(x => new { x.ProductId, x.WarehouseId })
            .IsUnique();

            builder.HasOne<WhereHouse>()
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
