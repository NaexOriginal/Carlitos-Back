using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Videogame
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Carpeta { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public Guid TutorId { get; set; }

    public virtual Tutor Tutor { get; set; } = null!;
}
