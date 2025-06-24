using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.TableName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Action).IsRequired().HasMaxLength(20);
            builder.Property(a => a.RecordId).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Changes).HasColumnType("TEXT");
            builder.Property(a => a.Timestamp).IsRequired();
            builder.Property(a => a.PerformedBy).HasMaxLength(100);
        }
    }
}
