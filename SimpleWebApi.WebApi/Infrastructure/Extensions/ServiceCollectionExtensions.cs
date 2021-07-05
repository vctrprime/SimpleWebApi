using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using SimpleWebApi.DAL.Config;
using SimpleWebApi.DAL.Connection.Abstract;
using SimpleWebApi.DAL.Connection.Concrete;
using SimpleWebApi.DAL.Repositories.Abstract.Common;
using SimpleWebApi.DAL.Repositories.Concrete.Common;
using SimpleWebApi.DAL.SqlQueries.Abstract;
using SimpleWebApi.Profiles.BreakingBad;
using SimpleWebApi.WebApi.Infrastructure.Handlers;

namespace SimpleWebApi.WebApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModules(this IServiceCollection services)
        {
            services
                .RegisterServices()
                .RegisterRepositories()
                .RegisterSqlQueries()
                .RegisterMapperProfiles();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton(new DbConfig { ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") });
            services.AddTransient<IConnectionCreator, ConnectionCreator>();
            
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            
            return services;
        }
        
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
            return services;
        }
        
        private static IServiceCollection RegisterSqlQueries(this IServiceCollection services)
        {
            var sqlAssembly = typeof(ISqlQuery).Assembly;

            services.RegisterAssemblyTypes(sqlAssembly)
                .Where(t => t.Name.EndsWith("SqlQuery")
                            && t.GetInterfaces().Any(ti =>
                                ti.Name == nameof(ISqlQuery)))
                .AsScoped()
                .Bind();

            services.RegisterAssemblyTypesByName(sqlAssembly,
                    t => t.EndsWith("RepositorySqlQueries"))
                .AsScoped()
                .PropertiesAutowired()
                .Bind();

            return services;
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