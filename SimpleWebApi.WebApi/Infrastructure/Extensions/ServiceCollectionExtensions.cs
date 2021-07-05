using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Profiles.BreakingBad;

namespace SimpleWebApi.WebApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModules(this IServiceCollection services)
        {
            services
                .RegisterMapperProfiles();
        }
        private static IServiceCollection RegisterMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(QuoteProfile)
            );

            return services;
        }
    }
}