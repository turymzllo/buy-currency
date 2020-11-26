using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Repositories;

namespace CurrencyExchange.Data.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CurrencyExchangeDbContext context)
            : base(context)
        {
        }
    }
}
