using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ticket");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AdminRemark)
                .HasColumnName("admin_remark");

            entity.Property(e => e.AdminRemarkDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("admin_remark_date");

            entity.Property(e => e.Attachment)
                .HasMaxLength(300)
                .HasColumnName("attachment");

            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .HasColumnName("email_id");

            entity.Property(e => e.PostingDate)
                .HasColumnName("posting_date");

            entity.Property(e => e.Prioprity)
                .HasMaxLength(300)
                .HasColumnName("prioprity");

            entity.Property(e => e.Status)
                .HasMaxLength(300)
                .HasColumnName("status");

            entity.Property(e => e.Subject)
                .HasMaxLength(300)
                .HasColumnName("subject");

            entity.Property(e => e.TaskType)
                .HasMaxLength(300)
                .HasColumnName("task_type");

            entity.Property(e => e.Ticket1)
                .HasColumnName("ticket");

            entity.Property(e => e.TicketId)
                .HasMaxLength(36)
                .HasColumnName("ticket_id");
        }
    }
}
