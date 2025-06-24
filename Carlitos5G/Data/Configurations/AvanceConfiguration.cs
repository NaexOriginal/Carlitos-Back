using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class AvanceConfiguration : IEntityTypeConfiguration<Avance>
    {
        public void Configure(EntityTypeBuilder<Avance> entity)
        {
            entity.HasNoKey();
            entity.ToTable("avance");

            entity.Property(e => e.ContentId)
                .HasMaxLength(36)
                .HasColumnName("content_id");

            entity.Property(e => e.PlaylistId)
                .HasMaxLength(36)
                .HasColumnName("playlist_id");

            entity.Property(e => e.Type)
                .HasMaxLength(400)
                .HasColumnName("type");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Content)
                .WithMany()
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Playlist)
                .WithMany()
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
