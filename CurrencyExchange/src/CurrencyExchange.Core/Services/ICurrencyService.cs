using CurrencyExchange.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Services
{
    public interface ICurrencyService
    {
        Task<Currency> GetCurrencyByISOCode(string isoCode);
        Task<decimal> GetPurchaseLimitById(int id);
    }
}
