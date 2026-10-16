using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesRead.Damain.SalesOrderProductReadModels;

namespace SalesRead.InfraStrucure.Data.Configurations
{
    public class SalesOrderProductReadModelConfiguration : IEntityTypeConfiguration<SalesOrderProductReadModel>
    {
        public void Configure(EntityTypeBuilder<SalesOrderProductReadModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SalesOrderId)
                   .IsRequired();
            builder.Property(x => x.OrderNumber)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(x => x.ProductId)
                   .IsRequired();
            builder.Property(x => x.Tax)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Total)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(x => x.ProductName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x=>x.Discount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(x => x.UnitPrice)
                     .IsRequired()
                     .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Quantity)
                   .IsRequired();
        }
    }
}
