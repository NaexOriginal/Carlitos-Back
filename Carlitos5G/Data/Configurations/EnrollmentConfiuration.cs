using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carlitos5G.Data.Configurations
{
  public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
  {
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
      //* Configuración de entidad de Enrollment
      builder.HasKey(e => e.Id);
      builder.Property(e => e.Id)
        .HasColumnName("Id")
        .HasMaxLength(36)
        .IsRequired();

      //* Relación con User
      builder.HasOne(e => e.User)
        .WithMany()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Restrict);

      //* Relación con Playlist
      builder.HasOne(e => e.Playlist)
        .WithMany()
        .HasForeignKey(e => e.PlaylistId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Property(e => e.EnrollmentDate)
        .HasColumnName("EnrollmentDate")
        .IsRequired();

      //* Asegurar inscripción unica por usuario
      builder.HasIndex(e => new { e.UserId, e.PlaylistId }).IsUnique();
    }
  }
}