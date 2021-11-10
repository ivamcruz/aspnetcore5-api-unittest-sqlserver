using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Business.Services;
using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Repositorys;

namespace Portal.DevTest.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ContextSQLServer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            return services;
        }
    }
}
