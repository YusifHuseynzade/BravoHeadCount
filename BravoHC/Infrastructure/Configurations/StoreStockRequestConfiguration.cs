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
    public class StoreStockRequestConfiguration : IEntityTypeConfiguration<StoreStockRequest>
    {
        public void Configure(EntityTypeBuilder<StoreStockRequest> builder)
        {
            builder.Property(s => s.RequestCount).IsRequired();
            builder.Property(s => s.CreatedBy).IsRequired();
            builder.HasOne(s => s.Uniform)
                   .WithMany(u => u.StoreStockRequests)
                   .HasForeignKey(s => s.UniformId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Employee)
                   .WithMany(e => e.StoreStockRequests)
                   .HasForeignKey(s => s.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
