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
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(s => s.Project)
                .WithMany(d => d.Sections)
                .HasForeignKey(s => s.ProjectId);

            builder.HasMany(s => s.SubSections)
                .WithOne(ss => ss.Section)
                .HasForeignKey(ss => ss.SectionId);

            builder.HasMany(s => s.Employees)
             .WithOne(e => e.Section)
             .HasForeignKey(e => e.SectionId);
        }
    }
}
