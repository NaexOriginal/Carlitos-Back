using Carlitos5G.Models;

public partial class Flipbook
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TutorId { get; set; }
    public Guid PlaylistId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Pages { get; set; }
    public string? Directory { get; set; }
    public string MediaPath { get; set; } = null!;
    public string ThumbnailPath { get; set; } = null!; // antes Thumb

    public string Status { get; set; } = null!;
    public string? Type { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;
    public virtual Tutor Tutor { get; set; } = null!;

}
