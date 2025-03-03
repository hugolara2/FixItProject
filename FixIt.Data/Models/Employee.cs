using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int Department { get; set; }

    public int Role { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> TicketAssignedtoNavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketCreatedbyNavigations { get; set; } = new List<Ticket>();
}
