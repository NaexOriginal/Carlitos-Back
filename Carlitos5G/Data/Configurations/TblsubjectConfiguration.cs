using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class TblsubjectConfiguration : IEntityTypeConfiguration<Tblsubject>
    {
        public void Configure(EntityTypeBuilder<Tblsubject> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("tblsubjects");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Creationdate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creationdate");

            entity.Property(e => e.PlaylistId)
                .HasMaxLength(36)
                .HasColumnName("playlist_id");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .HasColumnName("subject_name");

            entity.Property(e => e.UpdationDate)
                .HasColumnType("datetime")
                .HasColumnName("updation_date");

            entity.HasOne(d => d.Playlist)
                .WithMany(p => p.Tblsubjects)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
