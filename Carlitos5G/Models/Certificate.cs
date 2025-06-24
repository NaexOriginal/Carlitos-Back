using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Certificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? UserId { get; set; }

    public Guid? PlaylistId { get; set; }

    public string CertificatePath { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Playlist? Playlist { get; set; }

    public virtual User? User { get; set; }
}
