using Microsoft.EntityFrameworkCore;
using UserService.Domain.Employess;

namespace UserService.Infrastrucure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Empioyee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Empioyee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Department)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.JobTitle)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.HireDate)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithOne(u=>u.Employee)
                .HasForeignKey<Empioyee>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
