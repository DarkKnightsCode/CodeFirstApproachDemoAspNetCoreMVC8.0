using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachDemo.Models;

public class UserCredential
{
    [Key]
    public string UserName { get; set; }
    public string Password { get; set; }
}
