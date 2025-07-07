using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carlitos5G.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(150, ErrorMessage = "El correo electrónico no puede tener más de 150 caracteres.")]
        public string Email { get; set; } = null!;

        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        public string Password { get; set; } = null!;

        public string? Image { get; set; } // Para GET (ruta de la imagen)

        public IFormFile? ImageFile { get; set; } // Para POST/PUT (archivo)

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no es válido.")]
        public string Telefono { get; set; } = null!;

        public DateTime? FechaAsig { get; set; }

        [Required(ErrorMessage = "La etapa educativa es obligatoria.")]
        [StringLength(100, ErrorMessage = "La etapa educativa no puede tener más de 100 caracteres.")]
        public string EtapaEducativa { get; set; } = null!;

        public string? Grado { get; set; }

        public DateTime? UpdationDate { get; set; }

        public bool IsEdit { get; set; } = false;

    }

    // Agrega estos DTOs en Carlitos5G.Dtos
    public class UserActivityDto
    {
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public int TotalBookmarked { get; set; }
    }

    public class StudentCoursesDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public List<CourseProgressDto> Courses { get; set; } = new();
    }

    public class CourseProgressDto
    {
        public Guid PlaylistId { get; set; }
        public string Title { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public int TotalActivities { get; set; }
        public int CompletedActivities { get; set; }
        public int ProgressPercentage { get; set; }
    }

    public class AvailableCourseDto
    {
        public Guid PlaylistId { get; set; }
        public string Title { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string? Description { get; set; }
    }


}
