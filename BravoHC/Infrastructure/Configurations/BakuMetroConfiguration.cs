using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BakuMetroConfiguration : IEntityTypeConfiguration<BakuMetro>
    {
        public void Configure(EntityTypeBuilder<BakuMetro> builder)
        {
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(35);

            builder.HasMany(s => s.Employees)
               .WithOne(e => e.BakuMetro)
               .HasForeignKey(e => e.BakuMetroId);
        }
    }
}
