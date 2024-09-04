using CodeFirstApproachDemo.Models;

namespace CodeFirstApproachDemo.Repository;
public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(CompanyDbContext context) : base(context)
    {
        this.context = context;
    }

    public IEnumerable<Department> GetByDeptCode(string code)
    {
        return context.Departments.Where(x => x.DepartmentCode == code).ToList();
    }
}
