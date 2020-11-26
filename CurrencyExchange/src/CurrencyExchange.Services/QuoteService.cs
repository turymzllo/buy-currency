using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Repositories;
using CurrencyExchange.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICurrencyService _currencyService;
        public QuoteService(IHttpClientFactory clientFactory, ICurrencyService currencyService)
        {
            _clientFactory = clientFactory;
            _currencyService = currencyService;
        }

        public async Task<string> GetQuoteByCurrencyISOCode(string isoCode)
        {
            if (string.IsNullOrEmpty(isoCode))
            {
                throw new ArgumentNullException();
            }

            var currency = await _currencyService.GetCurrencyByISOCode(isoCode);
            
            if (currency == null)
            {
                throw new ArgumentException();
            }

            if (!string.IsNullOrEmpty(currency.ExchangeRateProvider) && !currency.UseUSDFactor)
            {

                var response = await _getQuoteFromProviderAsync(currency);

                return await response.Content.ReadAsStringAsync();
            }
            else if (currency.UseUSDFactor)
            {
                var usdCurrency = await _currencyService.GetCurrencyByISOCode("USD");                

                var response = await _getQuoteFromProviderAsync(usdCurrency);

                using var responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<string> quoteData = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(responseStream);
                if (quoteData.Count() == 3)
                {
                    decimal usdBuyQuote = decimal.Parse(quoteData.ElementAt(0), System.Globalization.CultureInfo.InvariantCulture);
                    decimal usdSellQuote = decimal.Parse(quoteData.ElementAt(1), System.Globalization.CultureInfo.InvariantCulture);
                    var result = new string[3];
                    result[0] = (usdBuyQuote * currency.USDFactor).ToString();
                    result[1] = (usdSellQuote * currency.USDFactor).ToString();
                    result[2] = quoteData.ElementAt(2);

                    return JsonSerializer.Serialize<IEnumerable<string>>(result);
                }

                return "0.00";
            }
            else
            {                
                throw new InvalidOperationException();
            }
        }

        private async Task<HttpResponseMessage> _getQuoteFromProviderAsync(Currency currency)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, currency.ExchangeRateProvider);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory");

            var cliente = _clientFactory.CreateClient();

            var response = await cliente.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}
