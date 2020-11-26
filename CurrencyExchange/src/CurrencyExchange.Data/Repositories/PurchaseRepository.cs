using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Data.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CurrencyExchangeDbContext context)
            : base(context)
        {
        }

        public Task<decimal> GetTotalPurchaseByUserCurrencyMonthAsync(int userId, int currencyId, DateTimeOffset date)
        {
            return Context.Set<Purchase>().Where(p => p.UserId == userId
                                                 && p.CurrencyId == currencyId
                                                 && p.Date.Year == date.Year
                                                 && p.Date.Month == date.Month)
                                          .SumAsync(p => p.PurchaseAmount);
        }
    }
}
