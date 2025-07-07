using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carlitos5G.Models;

public partial class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Image { get; set; } // 👈 para GET (ruta de la imagen)

    [NotMapped]
    public IFormFile? ImageFile { get; set; } // 👈 para POST/PUT (archivo)

    public string Genero { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime? FechaAsig { get; set; }

    public string EtapaEducativa { get; set; } = null!;

    public string? Grado { get; set; }

    public DateTime? UpdationDate { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Eventoscalendar> Eventoscalendars { get; set; } = new List<Eventoscalendar>();

    public virtual ICollection<Tblresult> Tblresults { get; set; } = new List<Tblresult>();

    public virtual ICollection<Usercheck> Userchecks { get; set; } = new List<Usercheck>();
}
