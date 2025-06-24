using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Seeders
{
    public static class AdminSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = Guid.NewGuid(),
                    Name = "Carlos Admin",
                    Email = "admin@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Image = "admin1.jpg",
                    Profession = "Coordinador",
                    UpdationDate = DateTime.UtcNow
                },
                new Admin
                {
                    Id = Guid.NewGuid(),
                    Name = "Luisa Manager",
                    Email = "luisa@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Image = "admin2.jpg",
                    Profession = "Gestora Educativa",
                    UpdationDate = DateTime.UtcNow
                }
            );
        }
    }
}
