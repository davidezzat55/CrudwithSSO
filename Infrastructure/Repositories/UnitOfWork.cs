using Infrastructure.Context;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProfileContext _dbContext;
        public UnitOfWork(ProfileContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
