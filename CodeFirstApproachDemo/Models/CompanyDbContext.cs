using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachDemo.Models;
public class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<UserCredential> userCredentials { get; set; }
}
