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
    public class ScheduledDataConfiguration : IEntityTypeConfiguration<ScheduledData>
    {
        public void Configure(EntityTypeBuilder<ScheduledData> builder)
        {
            builder.Property(sd => sd.Date)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow.AddHours(4));

            builder.Property(sd => sd.Plan)
                .IsRequired();

            builder.Property(sd => sd.Fact)
                .IsRequired();

            builder.Property(sd => sd.GraduationSchedule)
               .IsRequired()
               .HasMaxLength(100); 
            builder.Property(sd => sd.HolidayBalance)
                .IsRequired();

            builder.Property(sd => sd.GraduationBalance)
                .IsRequired();

            builder.HasOne(e => e.Project)
              .WithMany(s => s.ScheduledDatas)
              .HasForeignKey(e => e.ProjectId);

            builder.HasOne(e => e.Employee)
              .WithMany(s => s.ScheduledDatas)
              .HasForeignKey(e => e.EmployeeId);

        }
    }
}
