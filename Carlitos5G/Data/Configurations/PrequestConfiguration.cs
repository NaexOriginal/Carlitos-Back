using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class PrequestConfiguration : IEntityTypeConfiguration<Prequest>
    {
        public void Configure(EntityTypeBuilder<Prequest> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("prequest");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Company)
                .HasMaxLength(255)
                .HasColumnName("company");

            entity.Property(e => e.Contactno)
                .HasMaxLength(11)
                .HasColumnName("contactno");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Others)
                .HasMaxLength(255)
                .HasColumnName("others");

            entity.Property(e => e.PostingDate)
                .HasColumnType("datetime")
                .HasColumnName("posting_date");

            entity.Property(e => e.Query)
                .HasColumnType("text")
                .HasColumnName("query");

            entity.Property(e => e.Remark)
                .HasColumnType("text")
                .HasColumnName("remark");

            entity.Property(e => e.Services)
                .HasColumnType("text")
                .HasColumnName("services");

            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
        }
    }
}
