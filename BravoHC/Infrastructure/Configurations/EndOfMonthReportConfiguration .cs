using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EndOfMonthReportConfiguration : IEntityTypeConfiguration<EndOfMonthReport>
    {
        public void Configure(EntityTypeBuilder<EndOfMonthReport> builder)
        {
            builder.Property(e => e.MarketCodeAndName).IsRequired();
            builder.Property(e => e.EncashmentAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.DepositAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.PettyCashAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}
