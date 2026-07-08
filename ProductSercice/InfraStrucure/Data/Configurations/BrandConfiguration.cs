using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSercice.Domain.Brand;

namespace ProductSercice.InfraStrucure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x=>x.Country)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(x => x.Name)
                   .IsUnique();

        }
    }
}
