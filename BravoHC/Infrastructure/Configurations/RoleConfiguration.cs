using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(ar => ar.RoleName)
                 .IsRequired()
                 .HasMaxLength(50);

            builder.HasMany(ar => ar.AppUsers)
                .WithOne(au => au.Role)
                .HasForeignKey(au => au.RoleId);
        }
    }
}
