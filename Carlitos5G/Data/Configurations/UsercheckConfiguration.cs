using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class UsercheckConfiguration : IEntityTypeConfiguration<Usercheck>
    {
        public void Configure(EntityTypeBuilder<Usercheck> entity)
        {
            // Clave primaria
            entity.HasKey(e => e.Id);

            // Nombre de la tabla
            entity.ToTable("usercheck");

            // Propiedades
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.City)
                .HasColumnName("city")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Country)
                .HasColumnName("country")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Ip)
                .HasColumnName("ip")
                .HasMaxLength(16);

            entity.Property(e => e.Logindate)
                .HasColumnName("logindate")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Logintime)
                .HasColumnName("logintime")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Mac)
                .HasColumnName("mac")
                .HasMaxLength(16);

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(255)
                .IsUnicode(false);

            // Relación con la entidad User
            entity.HasOne(d => d.User)
                .WithMany(p => p.Userchecks)
                .HasForeignKey(d => d.UserId);
        }
    }

}
