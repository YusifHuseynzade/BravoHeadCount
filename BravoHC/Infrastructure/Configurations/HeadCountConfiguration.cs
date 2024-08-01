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
    public class HeadCountConfiguration : IEntityTypeConfiguration<HeadCount>
    {
        public void Configure(EntityTypeBuilder<HeadCount> builder)
        {

            builder.Property(t => t.HCNumber).IsRequired();
        }
    }
}
