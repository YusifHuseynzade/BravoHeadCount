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
    public class SummaryConfiguration : IEntityTypeConfiguration<Summary>
    {
        public void Configure(EntityTypeBuilder<Summary> builder)
        {
            builder.Property(s => s.Year)
          .IsRequired();

            builder.Property(s => s.WorkdaysCount)
                .IsRequired();

            builder.Property(s => s.VacationDaysCount)
                .IsRequired();

            builder.Property(s => s.SickDaysCount)
                .IsRequired();

            builder.Property(s => s.DayOffCount)
                .IsRequired();

            builder.Property(s => s.AbsentDaysCount)
                .IsRequired();

            // Employee ile ilişki
            builder.HasOne(s => s.Employee)
                .WithMany(e => e.Summaries)
                .HasForeignKey(s => s.EmployeeId);

            // Month ile ilişki
            builder.HasOne(s => s.Month)
                .WithMany(m => m.Summaries)
                .HasForeignKey(s => s.MonthId);
        }
    }
}
