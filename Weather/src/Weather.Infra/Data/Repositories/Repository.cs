using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weather.Core.Data;
using Weather.Core.DomainObjects;

namespace Weather.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity 
    {
        private readonly OpenWeatherContext _context;
        private readonly DbSet<TEntity> _entity;

        public Repository(OpenWeatherContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entity.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return await _entity.AsNoTracking().Where(where).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual void Update(TEntity entity)
        {
            _entity.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
