using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.Repository.Abstraction;

namespace TTechTask.DataAccess
{
    public class GenericRepository<T> :IGenericReposiory<T> where T:class
    {
        private readonly TTechTaskContext _context;
        private readonly DbSet<T> _table;
        public GenericRepository(TTechTaskContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _table;
        }
        public async Task<T> Get(int id)
        {
            var eneity = await _table.FindAsync(id);
            if (eneity == null)
                return null;
            _context.Entry(eneity).State = EntityState.Detached;
            return eneity;
        }

        public async Task<T> Add(T entity)
        {
            return (await _table.AddAsync(entity)).Entity;
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _table.AsNoTracking().Where(expression);
        }

        public void Remove(T entity)
        {
            _table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
        }

        public T Update(T entity)
        {
            return _table.Update(entity).Entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
        }
    }
}
