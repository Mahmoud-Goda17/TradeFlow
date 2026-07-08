using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSercice.Domain.Product;

namespace ProductSercice.InfraStrucure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x=>x.SKU)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x=>x.Barcode)
                .IsRequired()
                .HasMaxLength(18);
            builder.Property(x => x.CostPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.SellingPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasIndex(x => x.SKU)
                .IsUnique();

            builder.HasIndex(x => x.Barcode)
                .IsUnique(); 
            builder.HasIndex(x => new { x.Name, x.BrandId, x.CategoryId });
                

            builder.Property(x => x.Status)
                .IsRequired();
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
