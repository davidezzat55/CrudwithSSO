using LinkDev.Wasel.Core.Contracts;
using System.Linq.Expressions;

namespace Core.DominModels.UserAggregate
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<UserProfile> GetProfileAsync(Expression<Func<UserProfile, bool>> expression, CancellationToken cancellationToken);

    }

}
