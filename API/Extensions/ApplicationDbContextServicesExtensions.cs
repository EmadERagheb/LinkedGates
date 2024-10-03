using Microsoft.EntityFrameworkCore;
using System;

namespace API.Extensions
{
    public static class ApplicationDbContextServicesExtensions
    {
        public static IServiceCollection AddApplicationDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<QualityDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("QualityConnection"),
            //        setup => setup.CommandTimeout(30).EnableRetryOnFailure(
            //            maxRetryCount: 5,
            //            maxRetryDelay: TimeSpan.FromSeconds(5),
            //           errorNumbersToAdd: null));
            //    options.LogTo(Console.Write, LogLevel.Information);
            //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //    if (environment.IsDevelopment())
            //    {
            //        options.EnableSensitiveDataLogging();
            //        options.EnableDetailedErrors();
            //    }
            //}
            //      );

            return services;
        }
    }
}
