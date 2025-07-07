
namespace Carlitos5G.Models
{
  public class Enrollment
  {
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public Guid PlaylistId { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
    public virtual Playlist Playlist { get; set; } = null!;
  }
}