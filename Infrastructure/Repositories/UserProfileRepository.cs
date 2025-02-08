using Core.DominModels.UserAggregate;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        private readonly ProfileContext _dbContext;
        public UserProfileRepository(ProfileContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserProfile> GetProfileAsync(Expression<Func<UserProfile, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<UserProfile>()         
                   .AsNoTracking()
                   .FirstOrDefaultAsync(expression, cancellationToken);
        }
    }
}
