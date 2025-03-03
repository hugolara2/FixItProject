using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Role
{
    public int Positionid { get; set; }

    public string? Name { get; set; }

    public int Department { get; set; }

    public string? Description { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
