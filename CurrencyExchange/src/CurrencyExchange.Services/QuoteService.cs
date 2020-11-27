using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.ModelsDTO;
using CurrencyExchange.Core.Repositories;
using CurrencyExchange.Core.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

            QuoteDTO result;
            if (!string.IsNullOrEmpty(currency.ExchangeRateProvider) && !currency.UseUSDFactor)
            {
                result = await _getQuoteFromProviderAsync(currency);
            }
            else if (currency.UseUSDFactor)
            {
                var usdCurrency = await _currencyService.GetCurrencyByISOCode("USD");

                var usdQuote = await _getQuoteFromProviderAsync(usdCurrency);

                result = new QuoteDTO
                {
                    id = isoCode,
                    buy = usdQuote.buy * currency.USDFactor,
                    sale = usdQuote.sale * currency.USDFactor,
                    updated = usdQuote.updated
                };
            }
            else
            {
                throw new InvalidOperationException();
            }

            return JsonConvert.SerializeObject(result);
        }

        private async Task<QuoteDTO> _getQuoteFromProviderAsync(Currency currency)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, currency.ExchangeRateProvider);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory");

            var cliente = _clientFactory.CreateClient();

            var response = await cliente.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<string> quoteData = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<string>>(responseStream);
                if (quoteData.Count() == 3)
                {
                    return new QuoteDTO
                    {
                        id = currency.ISOCode,
                        buy = decimal.Parse(quoteData.ElementAt(0), System.Globalization.CultureInfo.InvariantCulture),
                        sale = decimal.Parse(quoteData.ElementAt(1), System.Globalization.CultureInfo.InvariantCulture),
                        updated = quoteData.ElementAt(2)
                    };
                }

                throw new NullReferenceException();
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}
