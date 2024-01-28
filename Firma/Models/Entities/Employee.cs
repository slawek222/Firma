using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Firma.Models.Entities;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Position { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Salary { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeesManager> EmployeesManagerEmployees { get; set; } = new List<EmployeesManager>();

    [InverseProperty("Manager")]
    public virtual ICollection<EmployeesManager> EmployeesManagerManagers { get; set; } = new List<EmployeesManager>();
}
