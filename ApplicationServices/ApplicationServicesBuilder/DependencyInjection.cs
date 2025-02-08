using ApplicationServices.Mapper;
using ApplicationServices.Services;
using Core.DominModels.UserAggregate;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace ApplicationServices.ApplicationServicesBuilder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddAutoMapper(typeof(ProfileMapper));
            return services;
        }
    }
}
