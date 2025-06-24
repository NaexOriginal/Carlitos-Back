using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carlitos5G.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id")
                .HasMaxLength(36)
                .IsRequired()
                
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(a => a.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(a => a.Password)
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(a => a.Image)
                .HasColumnName("Image")
                .HasMaxLength(500);
            builder.Property(a => a.Profession)
                .HasColumnName("Profession")
                .HasMaxLength(100);
            builder.Property(a => a.UpdationDate)
                .HasColumnName("UpdationDate");

            builder.HasIndex(a => a.Email).IsUnique();
        }
    }
}
