using System.Collections.Generic;

namespace MachineCloud.Domain
{
    public interface IRepository<T> where T : class
    {
        T Find(params object[] keys);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
