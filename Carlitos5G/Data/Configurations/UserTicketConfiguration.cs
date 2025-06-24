using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(EntityTypeBuilder<UserTicket> entity)
        {
            // Clave primaria
            entity.HasKey(e => e.Id);

            // Nombre de la tabla
            entity.ToTable("user_ticket");

            // Propiedades
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AltEmail)
                .HasColumnName("alt_email")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Gender)
                .HasColumnName("gender")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Mobile)
                .HasColumnName("mobile")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(255)
                .IsUnicode(false);

            // Fecha de publicación
            entity.Property(e => e.PostingDate)
                .HasColumnName("posting_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // En MySQL usamos CURRENT_TIMESTAMP en lugar de getdate()

            // Estado
            entity.Property(e => e.Status)
                .HasColumnName("status");
        }
    }

}
