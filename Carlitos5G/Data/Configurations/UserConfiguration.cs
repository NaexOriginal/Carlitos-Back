using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // Clave primaria
            entity.HasKey(e => e.Id);

            // Nombre de la tabla
            entity.ToTable("users");

            // Propiedades
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(50)  // Limite de longitud
                .IsUnicode(false);  // No es necesario en MySQL, se asume que es un string estándar

            entity.Property(e => e.EtapaEducativa)
                .HasColumnName("etapa_educativa")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.FechaAsig)
                .HasColumnName("fecha_asig")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // En MySQL usamos CURRENT_TIMESTAMP en lugar de getdate()

            entity.Property(e => e.Genero)
                .HasColumnName("genero")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Grado)
                .HasColumnName("grado")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Image)
                .HasColumnName("image")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(100)
                .HasDefaultValue("1234"); // Valor por defecto de ejemplo

            entity.Property(e => e.Telefono)
                .HasColumnName("telefono")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.UpdationDate)
                .HasColumnName("updationDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // En MySQL usamos CURRENT_TIMESTAMP en lugar de getdate()

            entity.HasIndex(a => a.Email).IsUnique();
        }
    }

}
