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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Badge)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.RecruiterComment)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.HasOne(e => e.Project)
              .WithMany(s => s.Employees)
              .HasForeignKey(e => e.ProjectId);

            builder.HasOne(e => e.Section)
              .WithMany(s => s.Employees)
              .HasForeignKey(e => e.SectionId);

            builder.HasOne(e => e.SubSection)
              .WithMany(s => s.Employees)
              .HasForeignKey(e => e.SubSectionId);

            builder.HasOne(e => e.Position)
              .WithMany(s => s.Employees)
              .HasForeignKey(e => e.PositionId);

            builder.HasOne(e => e.ResidentalArea)
             .WithMany(s => s.Employees)
             .HasForeignKey(e => e.ResidentalAreaId);

            builder.HasMany(s => s.ScheduledDatas)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeId);

            builder.HasMany(e => e.HeadCounts)
              .WithOne(hc => hc.Employee)
              .HasForeignKey(hc => hc.EmployeeId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
