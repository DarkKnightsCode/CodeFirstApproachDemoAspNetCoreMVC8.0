using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachDemo.Models;
public class Department
{
    [Key]
    public int DepartmentId { get; set; }
    [Required]
    public string DepartmentName { get; set; }
    [Required]
    public string DepartmentCode { get; set; } 
}
