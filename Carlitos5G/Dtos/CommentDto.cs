using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? ContentId { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El tutor es obligatorio.")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "El comentario no puede estar vacío.")]
        [StringLength(500, ErrorMessage = "El comentario no puede tener más de 500 caracteres.")]
        public string CommentText { get; set; } = null!;

        public DateTime? Date { get; set; } = DateTime.UtcNow;
    }
}
