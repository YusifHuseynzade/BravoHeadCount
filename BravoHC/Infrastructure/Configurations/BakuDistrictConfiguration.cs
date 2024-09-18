using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BakuDistrictConfiguration : IEntityTypeConfiguration<BakuDistrict>
    {
        public void Configure(EntityTypeBuilder<BakuDistrict> builder)
        {
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(35);

            builder.HasMany(s => s.Employees)
               .WithOne(e => e.BakuDistrict)
               .HasForeignKey(e => e.BakuDistrictId);
        }
    }
}
