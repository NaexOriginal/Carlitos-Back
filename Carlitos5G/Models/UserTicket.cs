using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class UserTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? AltEmail { get; set; }

    public string? Password { get; set; }

    public string? Mobile { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public int? Status { get; set; }

    public DateTime? PostingDate { get; set; }
}
