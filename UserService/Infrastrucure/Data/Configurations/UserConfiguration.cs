using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Users;

namespace UserService.Infrastrucure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.UserType)
                .IsRequired();
            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasIndex(m => new { m.Email, m.Phone })
                   .IsUnique();
            builder.HasIndex(m => m.FullName);

            builder.Navigation(x=>x.Addresses)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
