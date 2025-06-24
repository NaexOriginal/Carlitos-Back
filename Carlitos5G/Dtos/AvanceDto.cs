using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class AvanceDto
    {
        [Required(ErrorMessage = "El ID de la playlist es obligatorio.")]
        public Guid PlaylistId { get; set; }

        [Required(ErrorMessage = "El tipo de avance es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo no puede tener más de 50 caracteres.")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "El ID del contenido es obligatorio.")]
        public Guid ContentId { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid UserId { get; set; }
    }
}
