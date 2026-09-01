using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Domain.SalesOrders;

namespace SalesService.InfraStrucure.Data.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.OrderNumber)
                .IsUnique();

            builder.Property(x => x.CustomerId)
                .IsRequired();
            builder.Property(x => x.Status)
               .IsRequired();

            builder.Property(x => x.ShippingAddress)
            .IsRequired()
            .HasMaxLength(500);
            builder.Property(x => x.Subtotal)
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

            builder.Navigation(x => x.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x=>x.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
