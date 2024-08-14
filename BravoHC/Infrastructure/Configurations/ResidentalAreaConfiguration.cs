using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ResidentalAreaConfiguration : IEntityTypeConfiguration<ResidentalArea>
    {
        public void Configure(EntityTypeBuilder<ResidentalArea> builder)
        {
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.HasMany(s => s.Employees)
               .WithOne(e => e.ResidentalArea)
               .HasForeignKey(e => e.ResidentalAreaId);
        }
    }
}
