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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Email");

            builder.Property(t => t.UserName).IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Password).IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);

            builder.HasMany(u => u.AppUserRoles)
                   .WithOne(ur => ur.AppUser)
                   .HasForeignKey(ur => ur.AppUserId);

        }
    }
}
