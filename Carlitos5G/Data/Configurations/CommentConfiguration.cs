using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("comments");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CommentText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("comment");

            entity.Property(e => e.ContentId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("content_id");

            entity.Property(e => e.Date)
                .HasColumnName("date")
                .HasColumnType("datetime(6)") // Define precisión de 6
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");  // Usa CURRENT_TIMESTAMP con precisión


            entity.Property(e => e.TutorId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("tutor_id");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Tutor)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
