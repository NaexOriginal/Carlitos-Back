using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class OvaConfiguration : IEntityTypeConfiguration<Ova>
    {
        public void Configure(EntityTypeBuilder<Ova> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ovas");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Iframe)
                .HasMaxLength(255)
                .HasColumnName("iframe");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.PlaylistId)
                .HasMaxLength(20)
                .HasColumnName("playlist_id");

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Ovas)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
