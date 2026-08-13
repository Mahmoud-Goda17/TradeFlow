using InventoryService.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.InfraStrucure.Data.Configurations
{
    public class WhereHouseConfiguration : IEntityTypeConfiguration<WhereHouse>
    {
        public void Configure(EntityTypeBuilder<WhereHouse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasIndex(x => new {x.Code,x.Name})
                   .IsUnique();
        }
    }
}
