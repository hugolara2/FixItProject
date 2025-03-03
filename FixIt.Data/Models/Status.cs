using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Status
{
    public int Statusid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
