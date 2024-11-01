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
    public class BGSStockRequestConfiguration : IEntityTypeConfiguration<BGSStockRequest>
    {
        public void Configure(EntityTypeBuilder<BGSStockRequest> builder)
        {
            builder.Property(b => b.RequestCount).IsRequired();
            builder.Property(b => b.CreatedBy).IsRequired();

            builder.HasOne(b => b.Uniform)
                   .WithMany(u => u.BGSStockRequests)
                   .HasForeignKey(b => b.UniformId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Project)
                   .WithMany(p => p.BGSStockRequests)
                   .HasForeignKey(b => b.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
