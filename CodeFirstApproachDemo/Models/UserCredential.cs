using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachDemo.Models;

public class UserCredential
{
    [Key]
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
