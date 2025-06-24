using System;
using System.Collections.Generic;

namespace Carlitos5G.Models;

public partial class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? TicketId { get; set; }

    public Guid? EmailId { get; set; }

    public string? Subject { get; set; }

    public string? TaskType { get; set; }

    public string? Prioprity { get; set; }

    public string? Ticket1 { get; set; }

    public string? Attachment { get; set; }

    public string? Status { get; set; }

    public string? AdminRemark { get; set; }

    public DateTime? PostingDate { get; set; }

    public DateTime? AdminRemarkDate { get; set; }
}
