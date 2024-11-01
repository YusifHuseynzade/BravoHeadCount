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
    public class TransactionPageConfiguration : IEntityTypeConfiguration<TransactionPage>
    {
        public void Configure(EntityTypeBuilder<TransactionPage> builder)
        {
            builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Sender).IsRequired();
            builder.Property(t => t.SenderDate).IsRequired();
            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Transactions)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Employee)
                   .WithMany(e => e.Transactions)
                   .HasForeignKey(t => t.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Uniform)
                   .WithMany(u => u.Transactions)
                   .HasForeignKey(t => t.UniformId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
