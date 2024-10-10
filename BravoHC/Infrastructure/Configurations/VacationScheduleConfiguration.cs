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
    public class VacationScheduleConfiguration : IEntityTypeConfiguration<VacationSchedule>
    {
        public void Configure(EntityTypeBuilder<VacationSchedule> builder)
        {
            builder.Property(vs => vs.StartDate)
          .IsRequired();

            builder.Property(vs => vs.EndDate)
                .IsRequired();

            // Employee ile ilişki
            builder.HasOne(vs => vs.Employee)
                .WithMany(e => e.VacationSchedules)
                .HasForeignKey(vs => vs.EmployeeId);

            // ScheduledData ile ilişki
            builder.HasMany(vs => vs.ScheduledDatas)
                .WithOne(sd => sd.VacationSchedule)
                .HasForeignKey(sd => sd.VacationScheduleId);
        }
    }
}
