using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BakuTargetConfiguration : IEntityTypeConfiguration<BakuTarget>
    {
        public void Configure(EntityTypeBuilder<BakuTarget> builder)
        {
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(35);

            builder.HasMany(s => s.Employees)
               .WithOne(e => e.BakuTarget)
               .HasForeignKey(e => e.BakuTargetId);
        }
    }
}
