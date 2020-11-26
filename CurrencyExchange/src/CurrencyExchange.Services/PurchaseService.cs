using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.ModelsDTO;
using CurrencyExchange.Core.Repositories;
using CurrencyExchange.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyService _currencyService;
        private readonly IQuoteService _quoteService;
        public PurchaseService(IUnitOfWork unitOfWork, 
            ICurrencyService currencyService,
            IQuoteService quoteService)
        {
            _unitOfWork = unitOfWork;
            _currencyService = currencyService;
            _quoteService = quoteService;
        }

        public async Task<PurchaseDTO> CreatePurchase(PurchaseDTO newPurchase)
        {
            var currency = await _currencyService.GetCurrencyByISOCode(newPurchase.ISOCurrencyCode);
            if (currency == null)
                throw new ArgumentException();

            var response = await _quoteService.GetQuoteByCurrencyISOCode(newPurchase.ISOCurrencyCode);

            IEnumerable<string> quoteData = JsonSerializer.Deserialize<IEnumerable<string>>(response);
            if (quoteData == null && quoteData.Count() < 3)
               throw new NullReferenceException();

            var currentCuote = Convert.ToDecimal(quoteData.ElementAt(1), System.Globalization.CultureInfo.InvariantCulture);

            var purchase = new Purchase {
                UserId = newPurchase.UserId,
                AmountLC = newPurchase.AmountARGCurrency,
                CurrencyId = currency.Id,
                PurchaseAmount = newPurchase.AmountARGCurrency / currentCuote,
                Date = DateTimeOffset.Now
            };

            var canPurchase = await ValidAmaunt(purchase,currency.PurchaseLimit);
            if (canPurchase)
            {
                await _unitOfWork.Purchases.AddAsync(purchase);
                await _unitOfWork.CommitAsync();
                return newPurchase;

            }
            return null;
        }

        public async Task<bool> ValidAmaunt(Purchase purchase,decimal purchaseLimit)
        {            
            var purchaseTotal = await GetPurchaseAmountByUserCurrencyLastMonth(purchase);

            if (purchase.PurchaseAmount > (purchaseLimit - purchaseTotal))
                return false;

            return true;
        }

        private async Task<decimal> GetPurchaseAmountByUserCurrencyLastMonth(Purchase purchase)
        {
            var userId = purchase.UserId;
            var currencyId = purchase.CurrencyId;
            var date = purchase.Date;
            var purchases = await _unitOfWork.Purchases.GetTotalPurchaseByUserCurrencyMonthAsync(userId,currencyId,date);

            return purchases;
        }
    }
}
