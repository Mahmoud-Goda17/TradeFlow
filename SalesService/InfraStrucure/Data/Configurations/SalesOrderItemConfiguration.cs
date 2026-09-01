using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Domain.SalesOrderItems;

namespace SalesService.InfraStrucure.Data.Configurations
{
    public class SalesOrderItemConfiguration : IEntityTypeConfiguration<SalesOrderItem>
    {
        public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Tax)
               .IsRequired()
              .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Total)
             .IsRequired()
             .HasColumnType("decimal(18,2)");

            builder.HasIndex(x => new{ x.SalesOrderId, x.ProductId})
                  .IsUnique();
        }
    }
}
