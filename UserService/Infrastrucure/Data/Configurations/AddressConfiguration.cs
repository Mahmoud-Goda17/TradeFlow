using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Addresses;

namespace UserService.Infrastrucure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.BuildingNo)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.PostalCode)
                .HasMaxLength(100);
            builder.HasOne(a => a.User)
                .WithMany(u=>u.Addresses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
