using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TTechTash.Domain.Repository.Abstraction
{
    public interface IGenericReposiory<T> where T :class
    {
        Task<T> Get(int id);
        IEnumerable<T> Get();
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        T Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
