using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Like
{
    public Guid UserId { get; set; } 

    public Guid TutorId { get; set; }

    public Guid? ContentId { get; set; }

    public virtual Content? Content { get; set; }

    public virtual Tutor Tutor { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
