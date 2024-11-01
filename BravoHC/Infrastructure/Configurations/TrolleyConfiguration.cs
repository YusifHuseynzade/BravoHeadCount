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
    public class TrolleyConfiguration : IEntityTypeConfiguration<Trolley>
    {
        public void Configure(EntityTypeBuilder<Trolley> builder)
        {
            // Properties configuration
            builder.Property(t => t.CountDate)
                   .IsRequired();

            builder.Property(t => t.WorkingTrolleysCount)
                   .IsRequired();

            builder.Property(t => t.BrokenTrolleysCount)
                   .IsRequired();

            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Trolleys)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.TrolleyType)
                .WithMany(tt => tt.Trolleys)
                .HasForeignKey(t => t.TrolleyTypeId);
        }
    }
}
