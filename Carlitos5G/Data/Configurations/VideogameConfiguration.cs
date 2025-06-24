using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class VideogameConfiguration : IEntityTypeConfiguration<Videogame>
    {
        public void Configure(EntityTypeBuilder<Videogame> entity)
        {
            // Clave primaria
            entity.HasKey(e => e.Id);

            // Nombre de la tabla
            entity.ToTable("videogame");

            // Propiedades
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Carpeta)
                .HasColumnName("carpeta")
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.Imagen)
                .HasColumnName("imagen")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.Property(e => e.TutorId)
                .HasColumnName("tutor_id")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(15)
                .IsUnicode(false);

            // Relación con Tutor
            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Videogames)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
