﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ExpensesReportConfiguration : IEntityTypeConfiguration<ExpensesReport>
    {
        public void Configure(EntityTypeBuilder<ExpensesReport> builder)
        {
            builder.Property(e => e.TotalExpenses).HasColumnType("decimal(18,2)");
            builder.HasMany(s => s.Attachments)
            .WithOne(e => e.ExpensesReport)
            .HasForeignKey(e => e.ExpensesReportId);
        }
    }
}
