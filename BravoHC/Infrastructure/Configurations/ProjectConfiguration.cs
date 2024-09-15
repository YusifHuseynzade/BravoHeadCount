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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(t => t.ProjectCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.ProjectName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.IsStore).IsRequired(false);
            builder.Property(t => t.IsHeadOffice).IsRequired(false);

            builder.HasOne(d => d.FunctionalArea)
             .WithMany(fa => fa.Projects)
             .HasForeignKey(d => d.FunctionalAreaId);

            builder.HasMany(u => u.ProjectSections)
                 .WithOne(ur => ur.Project)
                 .HasForeignKey(ur => ur.ProjectId);

            builder.HasMany(s => s.Employees)
             .WithOne(e => e.Project)
             .HasForeignKey(e => e.ProjectId);

            builder.HasMany(s => s.ScheduledDatas)
            .WithOne(e => e.Project)
            .HasForeignKey(e => e.ProjectId);
        }
    }
}
