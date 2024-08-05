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
    public class FunctionalAreaConfiguration : IEntityTypeConfiguration<FunctionalArea>
    {
        public void Configure(EntityTypeBuilder<FunctionalArea> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(fa => fa.Projects)
                .WithOne(d => d.FunctionalArea)
                .HasForeignKey(d => d.FunctionalAreaId);
        }
    }
}
