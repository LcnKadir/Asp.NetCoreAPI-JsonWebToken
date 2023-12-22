using CoreLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class GenericRepository<TEntitiy> : IGenericRepository<TEntitiy> where TEntitiy : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntitiy> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntitiy>();
        }

        public async Task AddAsync(TEntitiy entitiy)
        {
            _dbset.AddAsync(entitiy);
        }

        public async Task<IEnumerable<TEntitiy>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<TEntitiy> GetByIdAsync(int id)
        {
            var entites = await _dbset.FindAsync(id);

            if(entites!=null)
            {
                _context.Entry(entites).State = EntityState.Detached;
            }

            return entites;
        }

        public void Remove(TEntitiy entitiy)
        {
            _dbset.Remove(entitiy);
        }

        public TEntitiy Update(TEntitiy entitiy)
        {
            _context.Entry(entitiy).State = EntityState.Modified;
            return entitiy;
        }

        public IQueryable<TEntitiy> Where(Expression<Func<TEntitiy, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
    }
}
