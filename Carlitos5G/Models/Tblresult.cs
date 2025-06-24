using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Tblresult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid StudentId { get; set; } 

    public Guid ClassId { get; set; }

    public string SubjectsId { get; set; } = null!;

    public string Marks { get; set; } = null!;

    public DateTime? PostingDate { get; set; }

    public DateTime? UpdationDate { get; set; }

    public virtual User Student { get; set; } = null!;
}
