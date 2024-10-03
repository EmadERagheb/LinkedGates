using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Data.Contracts
{
    public interface IGenericRepository<T, TContext> where T : BaseDomainModel where TContext : DbContext
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter, Expression<Func<T, object>>? order, params string[] properties);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? filter, bool tracked = false, params string[] properties);
        Task<int> GetCountAsync(Expression<Func<T, bool>>? filter);

        Task<bool> IsExistAsync(Expression<Func<T, bool>> filter);

        Task<TResult?> GetAsync<TResult>(Expression<Func<T, bool>> filter, bool tracked = false, params string[] properties);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);
        void Modify(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);




    }
}
