using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Firma.Models.Entities;

[Table("Employees_Managers")]
public partial class EmployeesManager
{
    [Key]
    [Column("EmployeeManagerID")]
    public int EmployeeManagerId { get; set; }

    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Column("ManagerID")]
    public int ManagerId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeesManagerEmployees")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("ManagerId")]
    [InverseProperty("EmployeesManagerManagers")]
    public virtual Employee Manager { get; set; } = null!;
}
