using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Repositories;
using CurrencyExchange.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class CurrencyService : ICurrencyService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CurrencyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Currency> GetCurrencyByISOCode(string isoCode)
        {
            return await _unitOfWork.Currencies.SingleOrDefaultAsync(c => c.ISOCode.Equals(isoCode));
        }

        public async Task<decimal> GetPurchaseLimitById(int id)
        {
            var result = await _unitOfWork.Currencies.SingleOrDefaultAsync(c => c.Id == id);

            if (result != null)
                return result.PurchaseLimit;

            return -1;
        }
    }
}
