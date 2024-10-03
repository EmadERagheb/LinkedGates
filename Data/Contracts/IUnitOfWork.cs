using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Contracts
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IGenericRepository<TEntity, TContext> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> CompleteAsync();
    }
}
