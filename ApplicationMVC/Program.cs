using ApplicationMVC.Extensions;
using Data.Contexts.Default;
using Data.Contracts;
using Data.Helper;
using Data.Repositories;
using Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ApplicationMVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddApplicationDbContextServices(builder.Configuration, builder.Environment);
            builder.Services.AddDbContext<DefaultDbContext>(options =>
            {
                string? connectionStrng = builder.Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionStrng))
                    throw new Exception("Connection string is not found");

                options.UseSqlServer(connectionStrng,
                    setup => setup.CommandTimeout(30).EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                       errorNumbersToAdd: null));
                options.LogTo(Console.Write, LogLevel.Information);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
            builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #region Insure Database Exists And Seed It Before Run Application
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            if (app.Environment.IsProduction())
            {
                var context = service.GetRequiredService<DefaultDbContext>();
                var logger = service.GetRequiredService<ILogger<Program>>();
                try
                {
                   await context.Database.MigrateAsync();
                   await DefaultDbContextSeeding.SeedingAsync(context);
                    logger.LogInformation("migration applied success");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during migration");

                }
            }

            #endregion
            app.Run();
        }
    }
}
