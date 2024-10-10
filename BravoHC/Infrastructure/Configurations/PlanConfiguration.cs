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
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Value)
               .IsRequired()
               .HasMaxLength(50); // Saat aralığı (örneğin, "06:30-15:30")

            builder.Property(p => p.Label)
                .IsRequired()
                .HasMaxLength(50); // Vardiyanın etiketi

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(7); // Renk kodu (örneğin, "#EAE3C8")

            builder.Property(p => p.Shift)
                .IsRequired()
                .HasMaxLength(20); // Vardiyanın türü

            // ScheduledData ile ilişki
            builder.HasMany(p => p.ScheduledDatas)
                .WithOne(sd => sd.Plan)
                .HasForeignKey(sd => sd.PlanId);
        }
    }
}
