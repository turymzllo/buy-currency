using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.ModelsDTO;
using System.Threading.Tasks;

namespace CurrencyExchange.Core.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDTO> CreatePurchase(PurchaseDTO newPurchase);
    }
}
