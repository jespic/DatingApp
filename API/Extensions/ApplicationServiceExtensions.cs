using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)//we want to return IServiceCollection, as the ConfigureService method of Startup.cs
        {
            services.AddScoped<ITokenService, TokenService>(); //added as scope because is scoped to the lifetime of the HTTP request
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection")); //db connection string
            });

            return services;
        }
    }
}