using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EncashmentConfiguration : IEntityTypeConfiguration<Encashment>
    {
        public void Configure(EntityTypeBuilder<Encashment> builder)
        {
            builder.Property(e => e.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(e => e.AmountFromSales).HasColumnType("decimal(18,2)");
            builder.Property(e => e.AmountFoundOnSite).HasColumnType("decimal(18,2)");
            builder.Property(e => e.SafeSurplus).HasColumnType("decimal(18,2)");
            builder.Property(e => e.SealNumber).IsRequired();

            builder.HasMany(s => s.Attachments)
         .WithOne(e => e.Encashment)
         .HasForeignKey(e => e.EncashmentId);
        }
    }
}
