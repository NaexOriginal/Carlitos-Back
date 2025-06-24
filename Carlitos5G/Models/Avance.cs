using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Avance
{
    public Guid PlaylistId { get; set; }

    public string Type { get; set; } = null!;

    public Guid ContentId { get; set; }

    public Guid UserId { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
