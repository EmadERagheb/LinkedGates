using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Contexts.Default
{
    public class DefaultDbContext(DbContextOptions<DefaultDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }
        public DbSet<DeviceProperty> DeviceProperties { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == "Data.Configurations.DefaultDbContext");
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseDomainModel>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
