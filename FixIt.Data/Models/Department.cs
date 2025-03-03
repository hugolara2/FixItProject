using System;
using System.Collections.Generic;

namespace FixIt.Data.Models;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
