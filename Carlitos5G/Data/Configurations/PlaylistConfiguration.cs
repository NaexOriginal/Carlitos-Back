using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("playlist");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasDefaultValue("nn")
                .HasColumnName("categoria");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            entity.Property(e => e.Iframe)
                .HasMaxLength(1000)
                .HasColumnName("iframe");

            entity.Property(e => e.IsDiplomado)
                .HasDefaultValue(false)
                .HasColumnName("is_diplomado");

            entity.Property(e => e.IsVisto)
                .HasDefaultValue(false)
                .HasColumnName("is_visto");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("deactive")
                .HasColumnName("status");

            entity.Property(e => e.Thumb)
                .HasMaxLength(500)
                .HasColumnName("thumb")
                .IsUnicode(false);

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.Property(e => e.TutorId)
                .HasMaxLength(36)
                .HasColumnName("tutor_id");

            entity.Property(e => e.UpdationDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("UpdationDate");

            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Playlists)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
