using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.HasNoKey();
            entity.ToTable("contact");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("message");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.Property(e => e.Number)
                .HasColumnName("number");
        }
    }
}
