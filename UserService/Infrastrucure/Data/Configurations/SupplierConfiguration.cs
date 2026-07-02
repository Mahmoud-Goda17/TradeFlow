using Microsoft.EntityFrameworkCore;
using UserService.Domain.Supliers;

namespace UserService.Infrastrucure.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.TaxNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(m => m.CompanyName);
            builder.HasOne(x => x.User)
                .WithOne(s=>s.Supplier)
                .HasForeignKey<Supplier>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
