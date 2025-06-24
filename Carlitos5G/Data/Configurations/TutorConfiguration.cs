using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> entity)
        {
            // Clave primaria
            entity.HasKey(e => e.Id);

            // Nombre de la tabla
            entity.ToTable("tutors");

            // Propiedades
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(50)  
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
                .IsUnicode(false);

            entity.Property(e => e.Profession)
                .HasColumnName("profession")
                .HasMaxLength(100)
                .IsUnicode(false);

            // Fechas y valores predeterminados
            entity.Property(e => e.UpdationDate)
                .HasColumnName("updationDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasIndex(a => a.Email).IsUnique();
        }
    }


}
