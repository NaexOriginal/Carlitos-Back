using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Carlitos5G.Data.Seeders
{
    public static class TutorSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutor>().HasData(
                new Tutor
                {
                    Id = Guid.NewGuid(),
                    Name = "Mario Pérez",
                    Email = "mario@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",  // Asegúrate de usar el hash correcto
                    Image = "/images/145d4cd3-a9c6-4ad7-aa36-3712fc918747.jpg",  // Ruta completa a la imagen
                    Profession = "Matemático",
                    UpdationDate = DateTime.UtcNow
                },
                new Tutor
                {
                    Id = Guid.NewGuid(),
                    Name = "Ana López",
                    Email = "ana@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",  // Asegúrate de usar el hash correcto
                    Image = "/images/145d4cd3-a9c6-4ad7-aa36-3712fc918748.jpg",  // Ruta completa a la imagen
                    Profession = "Física",
                    UpdationDate = DateTime.UtcNow
                }
            );
        }
    }
}
