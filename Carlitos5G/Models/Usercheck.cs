using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Usercheck
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Logindate { get; set; }

    public string? Logintime { get; set; }

    public Guid? UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public byte[]? Ip { get; set; }

    public byte[]? Mac { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual User? User { get; set; }
}
