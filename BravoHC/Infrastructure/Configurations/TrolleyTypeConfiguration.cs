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
    public class TrolleyTypeConfiguration : IEntityTypeConfiguration<TrolleyType>
    {
        public void Configure(EntityTypeBuilder<TrolleyType> builder)
        {
            builder.Property(tt => tt.Name)
                  .IsRequired()
                  .HasMaxLength(100); // Adjust length based on your needs

            builder.Property(tt => tt.Image)
                   .IsRequired()
                   .HasMaxLength(255); // Adjust length based on the expected URL length
            builder.HasMany(tt => tt.Trolleys)
                  .WithOne(t => t.TrolleyType)
                  .HasForeignKey(t => t.TrolleyTypeId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
