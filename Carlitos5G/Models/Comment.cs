namespace Carlitos5G.Models;

public partial class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? ContentId { get; set; }

    public Guid UserId { get; set; }

    public Guid TutorId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? Date { get; set; }

    public virtual Content? Content { get; set; }

    public virtual Tutor Tutor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
