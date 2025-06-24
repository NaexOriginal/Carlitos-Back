using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class BookmarkDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "El ID de la playlist es obligatorio.")]
        public Guid PlaylistId { get; set; }
    }
}
