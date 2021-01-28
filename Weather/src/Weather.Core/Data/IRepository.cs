using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Weather.Core.DomainObjects;

namespace Weather.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where);
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
