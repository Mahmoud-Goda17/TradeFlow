using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Customers;

namespace UserService.Infrastrucure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
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
               .WithOne(c=>c.Customer)
               .HasForeignKey<Customer>(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
