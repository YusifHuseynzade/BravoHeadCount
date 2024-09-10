﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class HeadCountBackgroundColorConfiguration : IEntityTypeConfiguration<HeadCountBackgroundColor>
    {
        public void Configure(EntityTypeBuilder<HeadCountBackgroundColor> builder)
        {
            builder.Property(t => t.ColorHexCode)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}