using CurrencyExchange.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.Data.Configuration
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseIdentityColumn();

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(c => c.ISOCode)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .Property(c => c.PurchaseLimit)
                .HasPrecision(18, 2);

            builder
                .Property(c => c.ExchangeRateProvider)
                .HasMaxLength(400);

            builder
                .Property(c => c.UseUSDFactor);

            builder
                .Property(c => c.USDFactor)
                .HasPrecision(18, 2);

            builder
                .ToTable("Currency");
        }
    }
}
