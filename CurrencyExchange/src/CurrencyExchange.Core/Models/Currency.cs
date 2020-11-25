using System;

namespace CurrencyExchange.Core.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public decimal PurchaseLimit { get; set; }
        public string ExchangeRateProvider { get; set; }
        public bool UseUSDFactor { get; set; }
        public decimal USDFactor { get; set; }
    }
}
