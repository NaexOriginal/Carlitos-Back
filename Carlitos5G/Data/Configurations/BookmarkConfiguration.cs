using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carlitos5G.Data.Configurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> entity)
        {
            // ✅ Definir clave compuesta
            entity.HasKey(e => new { e.UserId, e.PlaylistId });

            entity.ToTable("bookmark");

            entity.Property(e => e.PlaylistId)
                .HasColumnName("playlist_id");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Playlist)
                .WithMany()
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);
        }
    }
}
