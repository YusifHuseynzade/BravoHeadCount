using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MoneyOrderConfiguration : IEntityTypeConfiguration<MoneyOrder>
    {
        public void Configure(EntityTypeBuilder<MoneyOrder> builder)
        {
            builder.Property(m => m.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(m => m.HundredAZN).IsRequired();
        }
    }
}
