using Core.DominModels.UserAggregate;
using Infrastructure.Context;
using Infrastructure.Repositories;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.InfrastructureBuilder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence();
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ProfileContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
