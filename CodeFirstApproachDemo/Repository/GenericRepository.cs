
using CodeFirstApproachDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodeFirstApproachDemo.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected CompanyDbContext context;
    protected DbSet<T> table = null;
    public GenericRepository(CompanyDbContext context)
    {
        this.context = context;
        table = context.Set<T>();
    }
    public IEnumerable<T> GetAll()
    {
        return table.ToList();
    }

    public T GetById(int id)
    {
        return table.Find(id);  
    }

    public void Insert(T entity)
    {
        table.Add(entity);
    }

    public void Update(T entity)
    {
        table.Attach(entity);
        context.Entry(entity).State = EntityState.Modified; 
    }

    public void Delete(int id)
    {
        T existing = table.Find(id);
        table.Remove(existing);
    }

    public void Save()
    {
        context.SaveChanges();
    }
}

