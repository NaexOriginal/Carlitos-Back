using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Bookmark
{
    public Guid? UserId { get; set; }

    public Guid PlaylistId { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual User? User { get; set; }
}
