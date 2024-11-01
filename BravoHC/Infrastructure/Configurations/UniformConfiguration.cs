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
    public class UniformConfiguration : IEntityTypeConfiguration<Uniform>
    {
        public void Configure(EntityTypeBuilder<Uniform> builder)
        {
            builder.Property(u => u.UniCode).IsRequired();
            builder.Property(u => u.UniName).IsRequired();
            builder.Property(u => u.Gender).IsRequired();
            builder.Property(u => u.Size).IsRequired();
            builder.Property(u => u.UniType).IsRequired();
            builder.Property(u => u.UsageDuration).IsRequired();
        }
    }
}
