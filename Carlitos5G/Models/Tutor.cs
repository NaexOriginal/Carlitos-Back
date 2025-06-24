using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carlitos5G.Models;

public partial class Tutor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Image { get; set; } // 👈 para GET (ruta de la imagen)
    
    [NotMapped]
    public IFormFile? ImageFile { get; set; } // 👈 para POST/PUT (archivo)

    public DateTime? UpdationDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
    public virtual ICollection<Flipbook> Flipbooks { get; set; } = new List<Flipbook>();

    public virtual ICollection<Eventoscalendar> Eventoscalendars { get; set; } = new List<Eventoscalendar>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual ICollection<Videogame> Videogames { get; set; } = new List<Videogame>();
}
