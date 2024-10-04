// Ignore Spelling: env

using Data.Contexts.Default;
using Microsoft.EntityFrameworkCore;

namespace ApplicationMVC.Extensions
{
    public static class ApplicationDbContextServicesExtensions
    {
        public static IServiceCollection AddApplicationDbContextServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.AddDbContext<DefaultDbContext>(options =>
            {
                string? connectionStrng = config.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionStrng))
                    throw new Exception("Connection string is not found");

                options.UseSqlServer(connectionStrng,
                    setup => setup.CommandTimeout(30).EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                       errorNumbersToAdd: null));
                options.LogTo(Console.Write, LogLevel.Information);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });


            return services;
        }
    }
}
