using CurrencyExchange.Core.Models;
using CurrencyExchange.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Data
{
    public class CurrencyExchangeDbContext: DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CurrencyConfiguration());

            builder
                .ApplyConfiguration(new PurchaseConfiguration());
        }
    }
}
