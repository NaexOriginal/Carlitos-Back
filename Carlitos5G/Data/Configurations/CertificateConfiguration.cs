using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("certificates");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CertificatePath)
                .HasMaxLength(255)
                .HasColumnName("certificate_path");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.PlaylistId)
                .HasMaxLength(36)
                .HasColumnName("playlist_id");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Certificates)
                .HasForeignKey(d => d.PlaylistId);

            entity.HasOne(d => d.User)
                .WithMany(p => p.Certificates)
                .HasForeignKey(d => d.UserId);
        }
    }
}
