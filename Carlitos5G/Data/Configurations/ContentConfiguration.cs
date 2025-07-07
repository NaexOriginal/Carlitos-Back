using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("content");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TutorId)
                .HasColumnName("tutor_id")
                .HasMaxLength(36);

            entity.Property(e => e.PlaylistId)
                .HasColumnName("playlist_id")
                .HasMaxLength(36);

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            entity.Property(e => e.MediaPath)
                .HasColumnName("video")
                .HasMaxLength(100); // similar a video

            entity.Property(e => e.ThumbnailPath)
                .HasColumnName("thumb")
                .HasMaxLength(100);

            entity.Property(e => e.Date)
                .HasColumnName("date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("deactive");

            entity.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(15)
                .HasDefaultValue("Video");

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Contents)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Contents)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
