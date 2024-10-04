using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations.DefaultDbContext
{
    public class DevicePropertyConfiguration : IEntityTypeConfiguration<DeviceProperty>
    {
        public void Configure(EntityTypeBuilder<DeviceProperty> builder)
        {
           builder.HasKey(k => new { k.DeviceId, k.PropertyId });
        }
    }
}
