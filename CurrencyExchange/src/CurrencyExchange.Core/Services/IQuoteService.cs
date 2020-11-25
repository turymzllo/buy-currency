using System.Threading.Tasks;

namespace CurrencyExchange.Core.Services
{
    public interface IQuoteService
    {
        Task<string> GetQuoteByCurrencyISOCode(string isoCode);
    }
}
