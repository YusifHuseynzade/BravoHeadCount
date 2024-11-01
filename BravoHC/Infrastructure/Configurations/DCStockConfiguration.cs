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
    public class DCStockConfiguration : IEntityTypeConfiguration<DCStock>
    {
        public void Configure(EntityTypeBuilder<DCStock> builder)
        {
            builder.Property(d => d.StockCount).IsRequired();
            builder.Property(d => d.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(d => d.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(d => d.CreatedBy).IsRequired();
            builder.HasOne(d => d.Uniform)
                   .WithMany(u => u.DCStocks)
                   .HasForeignKey(d => d.UniformId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
