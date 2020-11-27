using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Core.ModelsDTO
{
    public class QuoteDTO
    {
        public string id { get; set; }
        public decimal buy { get; set; }
        public decimal sale { get; set; }
        public string updated { get; set; }
    }
}
