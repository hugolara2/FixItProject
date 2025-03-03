using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Ticket
{
    public int Ticketid { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Urgencyid { get; set; }

    public int Createdby { get; set; }

    public int? Assignedto { get; set; }

    public int Statusid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Closedat { get; set; }

    public DateTime? Duedate { get; set; }

    public virtual Employee? AssignedtoNavigation { get; set; }

    public virtual Employee CreatedbyNavigation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual Urgencylevel Urgency { get; set; } = null!;
}
