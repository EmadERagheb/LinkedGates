using Data.Contexts.Default;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Seeding
{
    public class DefaultDbContextSeeding
    {
        public static async Task SeedingAsync(DefaultDbContext context)
        {
            await SeedingCategory(context);
            await SeedingPropery(context);
            await SeedingDevice(context);
            await SeedingCategoryProperties(context);
            await SeedingDeviceProperties(context);
        }

        private static async Task SeedingCategory(DefaultDbContext context)
        {
           if(await context.Categories.AnyAsync()) return;
            var categoriesData = await File.ReadAllTextAsync(@"wwwroot\seeding data\categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            if (categories is null ) return;
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
        private static async Task SeedingPropery(DefaultDbContext context)
        {
            if(await context.Properties.AnyAsync()) return;
            var propertiesData = await File.ReadAllTextAsync(@"wwwroot\seeding data\properties.json");
            var properties = JsonSerializer.Deserialize<List<Property>>(propertiesData);
                if(properties is null ) return;
                await context.Properties.AddRangeAsync(properties);
            await context.SaveChangesAsync();
        }

        private static async Task SeedingDevice(DefaultDbContext context)
        {
            if (await context.Devices.AnyAsync()) return;
            var devicesData = await File.ReadAllTextAsync(@"wwwroot\seeding data\devices.json");
            var devices = JsonSerializer.Deserialize<List<Device>>(devicesData);
            if (devices is null) return;
            await context.Devices.AddRangeAsync(devices);
            await context.SaveChangesAsync();
        }
        private static async Task SeedingCategoryProperties(DefaultDbContext context)
        {
            if (await context.CategoryProperties.AnyAsync()) return;
            var categoryPropertiesData = await File.ReadAllTextAsync(@"wwwroot\seeding data\category properties.json");
            var categoryProperties = JsonSerializer.Deserialize<List<CategoryProperty>>(categoryPropertiesData);
            if (categoryProperties is null) return;
            await context.CategoryProperties.AddRangeAsync(categoryProperties);
            await context.SaveChangesAsync();
        }

        private static async Task SeedingDeviceProperties(DefaultDbContext context)
        {
            if (await context.DeviceProperties.AnyAsync()) return;
            var devicePropertiesData = await File.ReadAllTextAsync(@"wwwroot\seeding data\device properties.json");
            var deviceProperties = JsonSerializer.Deserialize<List<DeviceProperty>>(devicePropertiesData);
            if (deviceProperties is null) return;
            await context.DeviceProperties.AddRangeAsync(deviceProperties);
            await context.SaveChangesAsync();
        }

    }
}
