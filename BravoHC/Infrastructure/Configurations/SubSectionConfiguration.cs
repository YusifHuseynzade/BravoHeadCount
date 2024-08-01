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
    public class SubSectionConfiguration : IEntityTypeConfiguration<SubSection>
    {
        public void Configure(EntityTypeBuilder<SubSection> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(ss => ss.Section)
                .WithMany(s => s.SubSections)
                .HasForeignKey(ss => ss.SectionId);
        }
    }
}
