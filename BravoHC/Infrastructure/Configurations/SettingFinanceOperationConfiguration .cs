using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SettingFinanceOperationConfiguration : IEntityTypeConfiguration<SettingFinanceOperation>
    {
        public void Configure(EntityTypeBuilder<SettingFinanceOperation> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Branch).HasMaxLength(100);
        }
    }
}
