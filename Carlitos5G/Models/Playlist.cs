using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carlitos5G.Models;

public partial class Playlist
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TutorId { get; set; } 

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Thumb { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; } // 👈 para POST/PUT (archivo)

    public DateTime? Date { get; set; }

    public string? Status { get; set; }

    public string? Categoria { get; set; }

    public string? Iframe { get; set; }

    public DateTime? UpdationDate { get; set; }

    public bool? IsDiplomado { get; set; }

    public bool? IsVisto { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

    public virtual ICollection<Eventoscalendar> Eventoscalendars { get; set; } = new List<Eventoscalendar>();

    public virtual ICollection<Flipbook> Flipbooks { get; set; } = new List<Flipbook>();

    public virtual ICollection<Ova> Ovas { get; set; } = new List<Ova>();

    public virtual ICollection<Tblsubject> Tblsubjects { get; set; } = new List<Tblsubject>();

    public virtual Tutor Tutor { get; set; } = null!;
}
