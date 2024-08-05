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
    public class FormatConfiguration : IEntityTypeConfiguration<Format>
    {
        public void Configure(EntityTypeBuilder<Format> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Stores)
               .WithOne(e => e.Format)
               .HasForeignKey(e => e.FormatId);
        }
    }
}
