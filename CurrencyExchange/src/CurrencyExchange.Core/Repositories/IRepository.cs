using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
    }
}
