using Carlitos5G.Commons;
using Carlitos5G.Validation;
using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class PlaylistDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El ID del tutor es obligatorio.")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Description { get; set; } = null!;

        public string? Thumb { get; set; }

        [ConditionalImageValidation]
        public IFormFile? ImageFile { get; set; }


        public DateTime? Date { get; set; }

        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "La categoría no puede tener más de 50 caracteres.")]
        public string? Categoria { get; set; }

        [StringLength(1000, ErrorMessage = "El iframe no puede tener más de 1000 caracteres.")]
        public string? Iframe { get; set; }

        public DateTime? UpdationDate { get; set; }

        public bool? IsDiplomado { get; set; }
        public bool IsEdit { get; set; } = false;

    }

    public class ReassignPlaylistRequest
    {
        [Required]
        public Guid NewTutorId { get; set; }
    }

    public class PlaylistWithTutorDto
    {
        public Guid PlaylistId { get; set; }
        public string Title { get; set; }
        public Guid CurrentTutorId { get; set; }
        public string CurrentTutorName { get; set; }
        public string Status { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime? LastUpdated { get; set; }
    }

}
