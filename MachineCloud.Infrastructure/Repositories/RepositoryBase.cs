using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using MachineCloud.Domain;
using Ninject;

namespace MachineCloud.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TContext, T> : IRepository<T>
        where TContext : DbContext
        where T : class
    {
        protected abstract IDbSet<T> DbSet { get; }

        [Inject]
        public TContext Context { get; set; }

        public virtual T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public void Add(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
