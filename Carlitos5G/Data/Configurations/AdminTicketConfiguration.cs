using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carlitos5G.Data.Configurations
{
    public class AdminTicketConfiguration : IEntityTypeConfiguration<AdminTicket>
    {
        public void Configure(EntityTypeBuilder<AdminTicket> entity)
        {
            entity.ToTable("admin_ticket");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        }
    }
}
