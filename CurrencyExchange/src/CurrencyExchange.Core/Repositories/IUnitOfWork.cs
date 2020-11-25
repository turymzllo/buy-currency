using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICurrencyRepository Currencies { get; }
        IPurchaseRepository Purchases { get; }
        Task<int> CommitAsync();
    }
}
