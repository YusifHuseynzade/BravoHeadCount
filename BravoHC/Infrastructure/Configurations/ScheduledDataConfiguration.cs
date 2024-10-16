using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Configurations
{
    public class ScheduledDataConfiguration : IEntityTypeConfiguration<ScheduledData>
    {
        public void Configure(EntityTypeBuilder<ScheduledData> builder)
        {
            builder.Property(sd => sd.Date)
                .IsRequired(); // Date alanı zorunlu

            builder.Property(sd => sd.Fact)
                .IsRequired(false); // Fact alanı nullable

            // Plan ile ilişki
            builder.HasOne(sd => sd.Plan)
                .WithMany(p => p.ScheduledDatas)
                .HasForeignKey(sd => sd.PlanId);

            // Employee ile ilişki
            builder.HasOne(sd => sd.Employee)
                .WithMany(e => e.ScheduledDatas)
                .HasForeignKey(sd => sd.EmployeeId);

            // Project ile ilişki
            builder.HasOne(sd => sd.Project)
                .WithMany(p => p.ScheduledDatas)
                .HasForeignKey(sd => sd.ProjectId);

            // VacationSchedule ile ilişki
            builder.HasOne(sd => sd.VacationSchedule)
                .WithMany(vs => vs.ScheduledDatas)
                .HasForeignKey(sd => sd.VacationScheduleId)
                .IsRequired(false); // VacationSchedule opsiyonel
        }
    }
}
