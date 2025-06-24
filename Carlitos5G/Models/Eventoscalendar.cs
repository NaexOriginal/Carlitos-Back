using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Eventoscalendar
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? TutorId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? PlaylistId { get; set; }

    public string Evento { get; set; } = null!;

    public string ColorEvento { get; set; } = null!;

    public string FechaInicio { get; set; } = null!;

    public string FechaFin { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Playlist? Playlist { get; set; }

    public virtual Tutor? Tutor { get; set; }

    public virtual User? User { get; set; }
}
