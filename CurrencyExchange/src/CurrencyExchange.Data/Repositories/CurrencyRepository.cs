using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Data.Repositories
{
    public class CurrencyRepository: Repository<Currency>,ICurrencyRepository
    {
        public CurrencyRepository(CurrencyExchangeDbContext context)
            : base(context)
        {
        }
    }
}
