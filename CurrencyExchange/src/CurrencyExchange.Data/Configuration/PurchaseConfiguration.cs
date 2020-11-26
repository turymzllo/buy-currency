using CurrencyExchange.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CurrencyExchange.Data.Configuration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .UseIdentityColumn();

            builder
                .Property(p => p.UserId)
                .IsRequired();

            builder
                .Property(p => p.AmountLC)
                .IsRequired()
                .HasPrecision(18, 2);

            builder
                .Property(p => p.CurrencyId)
                .IsRequired();

            builder
                .Property(p => p.PurchaseAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder
                .Property(p => p.Date)
                .IsRequired();

            builder
                .ToTable("Purchase");
        }
    }
}
