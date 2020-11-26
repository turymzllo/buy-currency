using CurrencyExchange.Core.Models;
using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Repositories
{
    public interface IPurchaseRepository:IRepository<Purchase>
    {
        Task<decimal> GetTotalPurchaseByUserCurrencyMonthAsync(int userId, int currencyId, DateTimeOffset date);
    }
}
