using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Ova
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlaylistId { get; set; } 

    public string Name { get; set; } = null!;

    public string Iframe { get; set; } = null!;

    public virtual Playlist Playlist { get; set; } = null!;
}
