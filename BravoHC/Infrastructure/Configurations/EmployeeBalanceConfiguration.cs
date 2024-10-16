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
    public class EmployeeBalanceConfiguration : IEntityTypeConfiguration<EmployeeBalance>
    {
        public void Configure(EntityTypeBuilder<EmployeeBalance> builder)
        {
            builder.HasOne(sd => sd.Employee)
            .WithMany(e => e.EmployeeBalances)
            .HasForeignKey(sd => sd.EmployeeId);
        }
    }
}
