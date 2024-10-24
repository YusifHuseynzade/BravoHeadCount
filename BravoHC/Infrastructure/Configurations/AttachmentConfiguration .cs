using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FileUrl).IsRequired().HasMaxLength(500);
            builder.HasOne(a => a.Encashment)
                   .WithMany(e => e.Attachments)
                   .HasForeignKey(a => a.EncashmentId);
        }
    }
}
