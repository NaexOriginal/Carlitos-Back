using Carlitos5G.Validation;
using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class FlipbookDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El ID del tutor es obligatorio.")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "El ID de la playlist es obligatorio.")]
        public Guid PlaylistId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        public string Description { get; set; } = null!;

        public int? Pages { get; set; }

        public string? Directory { get; set; }

        public string? MediaPath { get; set; }

        public IFormFile? MediaFile { get; set; } = null!; // antes Archivo

        public string? ThumbnailPath { get; set; } = null!; // antes Thumb

        [ConditionalImageValidation]
        public IFormFile? ThumbnailFile { get; set; }

        public string Status { get; set; } = null!;

        [StringLength(50)]
        public string? Type { get; set; }

        public bool IsEdit { get; set; } = false;
    }
}
