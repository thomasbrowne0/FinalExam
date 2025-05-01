using Core.Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            // Get the appropriate connection string
            string connectionString;
            
            if (isDevelopment)
            {
                connectionString = configuration.GetConnectionString("DevelopmentConnection") ?? 
                    "Host=localhost;Database=postgres;Username=postgres;Password=postgres";
                Console.WriteLine($"Using Development connection: {connectionString}");
            }
            else
            {
                connectionString = configuration.GetConnectionString("ProductionConnection") ?? 
                    throw new InvalidOperationException("Production connection string not found.");
                Console.WriteLine("Using Production connection");
            }

            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Register interfaces
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
