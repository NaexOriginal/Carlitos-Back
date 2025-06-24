using Carlitos5G.Commons;
using Carlitos5G.Validation;

namespace Carlitos5G.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class TutorDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La profesión es obligatoria.")]
        [StringLength(100, ErrorMessage = "La profesion no puede tener más de 100 caracteres.")]
        public string Profession { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(3, ErrorMessage = "La contraseña debe tener al menos 3 caracteres.")]
        public string Password { get; set; } = null!;

        public string? Image { get; set; } // Ruta de la imagen solo para GET

        public IFormFile? ImageFile { get; set; } // Para POST/PUT

        public DateTime? UpdationDate { get; set; }

        public bool IsEdit { get; set; } = false;

    }



    public class TutorStatsDto
    {
        public Guid TutorId { get; set; }
        public int TotalPlaylists { get; set; }
        public int TotalContents { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
    }


    public class TutorSimpleDto
    {
        public Guid TutorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
    }
}
