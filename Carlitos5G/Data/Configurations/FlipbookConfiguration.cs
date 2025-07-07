using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class FlipbookConfiguration : IEntityTypeConfiguration<Flipbook>
    {
        public void Configure(EntityTypeBuilder<Flipbook> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("flipbook");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TutorId)
                .HasColumnName("tutor_id")
                .IsRequired();

            entity.Property(e => e.PlaylistId)
                .HasColumnName("playlist_id")
                .HasMaxLength(36)
                .IsRequired();


            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            entity.Property(e => e.Pages)
                .HasColumnName("pages");

            entity.Property(e => e.Directory)
                .HasColumnName("directory")
                .HasMaxLength(1000);

            entity.Property(e => e.MediaPath)
                .HasColumnName("archivo")
                .HasMaxLength(1000);

            entity.Property(e => e.ThumbnailPath)
                .HasColumnName("thumbnail_path")
                .HasMaxLength(1000);


            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(50);

            entity.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(15)
                .HasDefaultValue("Libro");


            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Flipbooks)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Flipbooks)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
