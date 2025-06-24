using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Juan Estudiante",
                    Email = "juan@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Image = "user1.jpg",
                    Genero = "Masculino",
                    Telefono = "555-1234",
                    FechaAsig = DateTime.UtcNow,
                    EtapaEducativa = "Primaria",
                    Grado = "4to",
                    UpdationDate = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Lucía Alumna",
                    Email = "lucia@carlitos.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    Image = "user2.jpg",
                    Genero = "Femenino",
                    Telefono = "555-5678",
                    FechaAsig = DateTime.UtcNow,
                    EtapaEducativa = "Secundaria",
                    Grado = "2do",
                    UpdationDate = DateTime.UtcNow
                }
            );
        }
    }
}
