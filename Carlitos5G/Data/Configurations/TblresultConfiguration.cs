using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class TblresultConfiguration : IEntityTypeConfiguration<Tblresult>
    {
        public void Configure(EntityTypeBuilder<Tblresult> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("tblresult");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ClassId)
                .HasMaxLength(36)
                .HasColumnName("class_id");

            entity.Property(e => e.Marks)
                .HasMaxLength(50)
                .HasColumnName("marks");

            entity.Property(e => e.PostingDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("posting_date");

            entity.Property(e => e.StudentId)
                .HasMaxLength(36)
                .HasColumnName("student_id");

            entity.Property(e => e.SubjectsId)
                .HasMaxLength(100)
                .HasColumnName("subjects_id");

            entity.Property(e => e.UpdationDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updation_date");

            entity.HasOne(d => d.Student)
                .WithMany(p => p.Tblresults)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
