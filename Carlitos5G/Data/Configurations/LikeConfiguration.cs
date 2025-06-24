using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> entity)
        {
            entity.HasNoKey();

            entity.ToTable("likes");

            entity.Property(e => e.ContentId)
                .HasMaxLength(36)
                .HasColumnName("content_id");

            entity.Property(e => e.TutorId)
                .HasMaxLength(36)
                .HasColumnName("tutor_id");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Content)
                .WithMany()
                .HasForeignKey(d => d.ContentId);

            entity.HasOne(d => d.Tutor)
                .WithMany()
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
