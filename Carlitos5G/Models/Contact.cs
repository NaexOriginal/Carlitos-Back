using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Contact
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Number { get; set; }

    public string Message { get; set; } = null!;
}
