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
    public class MonthConfiguration : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.Property(m => m.Number)
                 .IsRequired()
                 .HasMaxLength(2); // Ay numarası (1-12), maksimum 2 karakter

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(20); // Ay adı maksimum 20 karakter

            // Summary ile ilişki
            builder.HasMany(m => m.Summaries)
                .WithOne(s => s.Month)
                .HasForeignKey(s => s.MonthId);
        }
    }
}
