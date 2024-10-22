using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class FactConfiguration : IEntityTypeConfiguration<Fact>
    {
        public void Configure(EntityTypeBuilder<Fact> builder)
        {
            builder.Property(p => p.Value)
               .IsRequired()
               .HasMaxLength(50); // Saat aralığı (örneğin, "06:30-15:30")

            // ScheduledData ile ilişki
            builder.HasMany(p => p.ScheduledDatas)
                .WithOne(sd => sd.Fact)
                .HasForeignKey(sd => sd.FactId);
        }
    }
}
