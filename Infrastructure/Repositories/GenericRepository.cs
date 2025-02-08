using Core.DominModels.UserAggregate;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IAggregate
    {
        private readonly DbContext _dbContext;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


        public virtual void DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetWithFilterAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync(cancellationToken);
        }
    }

}
