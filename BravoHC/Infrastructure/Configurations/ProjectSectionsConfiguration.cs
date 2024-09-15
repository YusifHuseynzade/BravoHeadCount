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
    public class ProjectSectionsConfiguration : IEntityTypeConfiguration<ProjectSections>
    {
        public void Configure(EntityTypeBuilder<ProjectSections> builder)
        {
            builder.HasOne(au => au.Project)
                .WithMany(u => u.ProjectSections)
                .HasForeignKey(au => au.ProjectId)
                .IsRequired();

            builder.HasOne(au => au.Section)
                .WithMany(n => n.ProjectSections)
                .HasForeignKey(au => au.SectionId)
                .IsRequired();
        }
    }
}
