using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Wasel.Core.Contracts
{
    public interface IRepository<T> where T : class, IAggregate
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);

         void UpdateAsync(T entity);

        void DeleteAsync(T entity);

        Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);


        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<List<T>> GetWithFilterAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    }
}
