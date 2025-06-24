using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class AdminTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
