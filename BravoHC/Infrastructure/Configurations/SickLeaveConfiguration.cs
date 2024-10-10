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
    public class SickLeaveConfiguration : IEntityTypeConfiguration<SickLeave>
    {
        public void Configure(EntityTypeBuilder<SickLeave> builder)
        {
            builder.Property(sl => sl.StartDate)
            .IsRequired();

            builder.Property(sl => sl.EndDate)
                .IsRequired();

            // Employee ile ilişki
            builder.HasOne(sl => sl.Employee)
                .WithMany(e => e.SickLeaves)
                .HasForeignKey(sl => sl.EmployeeId);
        }
    }
}
