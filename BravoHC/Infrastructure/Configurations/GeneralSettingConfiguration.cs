using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Configurations
{
    public class GeneralSettingConfiguration : IEntityTypeConfiguration<GeneralSetting>
    {
        public void Configure(EntityTypeBuilder<GeneralSetting> builder)
        {
            // Configure EndOfMonthReportSettings as an owned type
            builder.OwnsOne(gs => gs.EndOfMonthReportSettings, eom =>
            {
                eom.WithOwner();

                eom.Property(r => r.SendingFrequency)
                    .HasColumnName("EndOfMonthSendingFrequency");

                eom.Property(r => r.Receivers)
                    .HasColumnName("EndOfMonthReceivers")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                eom.Property(r => r.ReceiversCC)
                    .HasColumnName("EndOfMonthReceiversCC")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                eom.Property(r => r.AvailableCreatedDays)
                    .HasColumnName("EndOfMonthAvailableCreatedDays")
                    .HasConversion(
                        v => string.Join(',', v.Select(i => i.ToString())),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<int>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                eom.Property(r => r.SendingTimes)
                    .HasColumnName("EndOfMonthSendingTimes")
                    .HasConversion(
                        v => string.Join(',', v.Select(t => t.ToString())),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(TimeSpan.Parse).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<TimeSpan>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));
            });

            // Configure ExpenseReportSettings as an owned type
            builder.OwnsOne(gs => gs.ExpenseReportSettings, exp =>
            {
                exp.WithOwner();

                exp.Property(r => r.SendingFrequency)
                    .HasColumnName("ExpenseSendingFrequency");

                exp.Property(r => r.Receivers)
                    .HasColumnName("ExpenseReceivers")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                exp.Property(r => r.ReceiversCC)
                    .HasColumnName("ExpenseReceiversCC")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                exp.Property(r => r.AvailableCreatedDays)
                    .HasColumnName("ExpenseAvailableCreatedDays")
                    .HasConversion(
                        v => string.Join(',', v.Select(i => i.ToString())),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<int>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                exp.Property(r => r.SendingTimes)
                    .HasColumnName("ExpenseSendingTimes")
                    .HasConversion(
                        v => string.Join(',', v.Select(t => t.ToString())),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(TimeSpan.Parse).ToList()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<TimeSpan>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));
            });

            builder.ToTable("GeneralSettings");
        }
    }
}
