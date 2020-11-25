using CurrencyExchange.Core.Models;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Services
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchase(Purchase newPurchase);
    }
}
