using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.Repository.Abstraction;
using TTechTash.Domain.Models;

namespace TTechTask.DataAccess.TypeRepository
{
    public class ProductRepoistory: GenericRepository<Product>, IProductRepoistory
    {
        public ProductRepoistory(TTechTaskContext context) : base(context)
        {

        }
        
    }
}
