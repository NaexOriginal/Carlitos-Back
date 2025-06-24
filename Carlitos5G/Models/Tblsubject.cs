using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Tblsubject
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlaylistId { get; set; } 

    public string SubjectName { get; set; } = null!;

    public DateTime? Creationdate { get; set; }

    public DateTime? UpdationDate { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;
}
