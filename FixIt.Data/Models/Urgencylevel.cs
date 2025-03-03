using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Urgencylevel
{
    public int Urgencylevelid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public short Priority { get; set; }

    public TimeSpan Resolutiontime { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
