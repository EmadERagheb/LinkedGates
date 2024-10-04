using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts.Default
{
    public class DefaultDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        public DefaultDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>();
            optionsBuilder.UseSqlServer("Data Source=Emad;Initial Catalog=LinkedGatesDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=True");
            return new DefaultDbContext(optionsBuilder.Options);
        }
    }
}
