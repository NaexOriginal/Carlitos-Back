
using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Dtos
{
  public class Enrollment
  {
    [Required(ErrorMessage = "El ID de usuario es obligatorio")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "El ID de la playlist es obligatoria")]
    public Guid PlaylistId { get; set; }

    public DateTime? EnrollmentDate { get; set; }
  }
}