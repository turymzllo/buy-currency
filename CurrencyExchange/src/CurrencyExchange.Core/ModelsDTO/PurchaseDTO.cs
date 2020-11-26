using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Core.ModelsDTO
{
    public class PurchaseDTO
    {
        public int UserId { get; set; }
        public string ISOCurrencyCode { get; set; }
        public decimal AmountARGCurrency { get; set; }
    }
}
