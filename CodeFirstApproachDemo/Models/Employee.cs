using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproachDemo.Models;
public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [Required]
    public string First_Name { get; set; } = default;
    [Required]
    public string Last_Name { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; } = default;
}
