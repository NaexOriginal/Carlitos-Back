using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class EventoscalendarConfiguration : IEntityTypeConfiguration<Eventoscalendar>
    {
        public void Configure(EntityTypeBuilder<Eventoscalendar> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("eventoscalendar");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ColorEvento)
                .HasMaxLength(20)
                .HasColumnName("color_evento");

            entity.Property(e => e.Estado)
                .HasColumnName("estado");

            entity.Property(e => e.Evento)
                .HasMaxLength(250)
                .HasColumnName("evento");

            entity.Property(e => e.FechaFin)
                .HasMaxLength(20)
                .HasColumnName("fecha_fin");

            entity.Property(e => e.FechaInicio)
                .HasMaxLength(20)
                .HasColumnName("fecha_inicio");

            entity.Property(e => e.PlaylistId)
                .HasMaxLength(36)
                .HasColumnName("playlist_id");

            entity.Property(e => e.TutorId)
                .HasMaxLength(36)
                .HasColumnName("tutor_id");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Eventoscalendars)
                .HasForeignKey(d => d.PlaylistId);

            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Eventoscalendars)
                .HasForeignKey(d => d.TutorId);

            entity.HasOne(d => d.User)
                .WithMany(p => p.Eventoscalendars)
                .HasForeignKey(d => d.UserId);
        }
    }
}
