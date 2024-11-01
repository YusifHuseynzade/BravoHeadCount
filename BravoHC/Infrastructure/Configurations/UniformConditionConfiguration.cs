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
    public class UniformConditionConfiguration : IEntityTypeConfiguration<UniformCondition>
    {
        public void Configure(EntityTypeBuilder<UniformCondition> builder)
        {
            builder.Property(u => u.UniName).IsRequired();
            builder.Property(u => u.UsageDuration).IsRequired();
            builder.Property(u => u.CreatedBy).IsRequired();
            builder.HasOne(u => u.Position)
                   .WithMany(p => p.UniformConditions)
                   .HasForeignKey(u => u.PositionId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
