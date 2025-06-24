using Carlitos5G.Models;

public partial class Content
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TutorId { get; set; }
    public Guid PlaylistId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string MediaPath { get; set; } = null!; // antes Video
    public string ThumbnailPath { get; set; } = null!; // antes Thumb
    public DateTime? Date { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;
    public virtual Tutor Tutor { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
