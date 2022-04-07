using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.Repository.Abstraction;
using TTechTask.DataAccess.TypeRepository;

namespace TTechTask.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepoistory ProductRepoistory { get; private set; }
        private readonly TTechTaskContext _context;
        public UnitOfWork(TTechTaskContext context)
        {
            _context = context;
            ProductRepoistory = new ProductRepoistory(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
