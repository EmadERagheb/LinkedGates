using Microsoft.EntityFrameworkCore;

namespace Data.Contexts.Default
{
    public class DefaultDbContext(DbContextOptions<DefaultDbContext> options) : DbContext(options)
    {

    }
}
