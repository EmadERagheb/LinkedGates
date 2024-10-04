using AutoMapper;
using Data.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Data.Repositories
{
    public class UnitOfWork<TContext>(TContext context, IMapper mapper) : IUnitOfWork<TContext> where TContext : DbContext
    {
        private Hashtable? _repositorise;
        public async Task<int> CompleteAsync()
        {
         return  await  context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity, TContext> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositorise is null) _repositorise = new Hashtable();
            var type = typeof(TEntity);
            var contextType = typeof(TContext);
            if (!_repositorise.ContainsKey($"{contextType.Name} - {type.Name}"))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type,contextType),context,mapper);
                _repositorise[$"{contextType.Name} - {type.Name}"] = repositoryInstance;

            }
            return (IGenericRepository<TEntity, TContext>)_repositorise[$"{contextType.Name} - {type.Name}"]!;
        }
    }
}
