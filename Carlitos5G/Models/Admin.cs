using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Models
{
    public partial class Admin
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [MaxLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede tener más de 100 caracteres")]
        public string Password { get; set; } = null!;

        public string Image { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "La profesión no puede tener más de 100 caracteres")]
        public string Profession { get; set; } = null!;

        public DateTime? UpdationDate { get; set; }
    }
}
