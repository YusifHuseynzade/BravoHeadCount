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
    public class DepartmentConfiguration: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.FunctionalArea)
                .WithMany(fa => fa.Departments)
                .HasForeignKey(d => d.FunctionalAreaId);

            builder.HasMany(d => d.Sections)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DepartmentId);
        }
    }
}
