using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class LikeDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El ID del tutor es obligatorio.")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "El ID del contenido es obligatorio.")]
        public Guid? ContentId { get; set; }

        [StringLength(100, ErrorMessage = "El nombre del usuario no puede tener más de 100 caracteres.")]
        public string? UserName { get; set; }

        [StringLength(100, ErrorMessage = "El nombre del tutor no puede tener más de 100 caracteres.")]
        public string? TutorName { get; set; }

        [StringLength(255, ErrorMessage = "El título del contenido no puede tener más de 255 caracteres.")]
        public string? ContentTitle { get; set; }
    }
}
