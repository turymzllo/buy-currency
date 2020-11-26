using CurrencyExchange.Core.Repositories;
using CurrencyExchange.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CurrencyExchangeDbContext _context;
        private CurrencyRepository _currencyRepository;
        private PurchaseRepository _purchaseRepository;

        public UnitOfWork(CurrencyExchangeDbContext context)
        {
            _context = context;
        }

        public ICurrencyRepository Currencies => _currencyRepository ??= new CurrencyRepository(_context);

        public IPurchaseRepository Purchases => _purchaseRepository ??= new PurchaseRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
