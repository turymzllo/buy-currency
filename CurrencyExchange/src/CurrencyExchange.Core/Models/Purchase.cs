using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Core.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal AmountLC { get; set; }
        public int CurrencyId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
