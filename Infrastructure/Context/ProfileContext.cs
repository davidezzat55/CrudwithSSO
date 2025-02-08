using Core.DominModels.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

}
