using CodeFirstApproachDemo.Models;

namespace CodeFirstApproachDemo.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public IEnumerable<Department> GetByDeptCode(string code);
    }
}
