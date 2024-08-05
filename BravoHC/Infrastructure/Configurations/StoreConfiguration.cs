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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(t => t.HeadCountNumber)
                .IsRequired();


            builder.HasMany(s => s.Employees)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            builder.HasOne(e => e.Format)
               .WithMany(s => s.Stores)
               .HasForeignKey(e => e.FormatId);
        }
    }
}
