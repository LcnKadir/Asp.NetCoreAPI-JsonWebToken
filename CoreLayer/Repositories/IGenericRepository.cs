using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repository
{
    public interface IGenericRepository<TEntitiy> where TEntitiy : class
    {
        Task<TEntitiy> GetByIdAsync(int id);

        Task<IEnumerable<TEntitiy>> GetAllAsync();

        IQueryable<TEntitiy> Where(Expression<Func<TEntitiy, bool>> predicate);

        Task AddAsync(TEntitiy entitiy);

        void Remove(TEntitiy entitiy);

    }
}
