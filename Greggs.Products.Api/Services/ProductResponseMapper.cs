using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services;

public class ProductResponseMapper : IProductResponseMapper
{
    private readonly IExchangeRateProvider _exchangeRateProvider;

    public ProductResponseMapper(IExchangeRateProvider exchangeRateProvider)
    {
        _exchangeRateProvider = exchangeRateProvider;
    }

    public IEnumerable<ProductResponseDto> Map(IEnumerable<Product> products, Currency currency)
    {
        var rate = currency == Currency.Eur ? _exchangeRateProvider.GetGbpToEurRate() : 0m;

        return products.Select(product => new ProductResponseDto
        {
            Name = product.Name,
            Currency = currency,
            PriceInPounds = currency == Currency.Gbp ? product.PriceInPounds : null,
            PriceInEuros = currency == Currency.Eur
                ? Math.Round(product.PriceInPounds * rate, 2, MidpointRounding.AwayFromZero)
                : null
        });
    }
}
