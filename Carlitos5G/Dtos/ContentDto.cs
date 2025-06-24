using Carlitos5G.Validation;
using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
    public class ContentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required(ErrorMessage = "El ID del tutor es obligatorio.")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "El ID de la playlist es obligatorio.")]
        public Guid PlaylistId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede tener más de 1000 caracteres.")]
        public string Description { get; set; } = null!;

        public string? MediaPath { get; set; } = null!; // antes Video

        public IFormFile? MediaFile { get; set; }

        public string? ThumbnailPath { get; set; } = null!; // antes Thumb

        [ConditionalImageValidation]
        public IFormFile? ThumbnailFile { get; set; }

        public DateTime? Date { get; set; } = DateTime.UtcNow;

        public string? Status { get; set; }

        [StringLength(50)]
        public string? Type { get; set; }

        public bool IsEdit { get; set; } = false;
    }

    public class ContentStatsDto
    {
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
    }


}
