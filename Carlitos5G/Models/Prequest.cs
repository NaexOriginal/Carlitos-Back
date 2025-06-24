using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Prequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Contactno { get; set; }

    public string? Company { get; set; }

    public string? Services { get; set; }

    public string? Others { get; set; }

    public string? Query { get; set; }

    public bool? Status { get; set; }

    public DateTime? PostingDate { get; set; }

    public string? Remark { get; set; }
}
