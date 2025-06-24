using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carlitos5G.Data.Configurations
{
    public class DownloadConfiguration : IEntityTypeConfiguration<Download>
    {
        public void Configure(EntityTypeBuilder<Download> entity)
        {
            entity.ToTable("download");

            entity.HasNoKey();

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date");

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
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
