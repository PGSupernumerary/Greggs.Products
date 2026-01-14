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

    public IEnumerable<ProductResponseDto> Map(IEnumerable<Product> products)
    {
        var rate = _exchangeRateProvider.GetGbpToEurRate();

        return products.Select(product => new ProductResponseDto
        {
            Name = product.Name,
            PriceInPounds = product.PriceInPounds,
            PriceInEuros = Math.Round(product.PriceInPounds * rate, 2, MidpointRounding.AwayFromZero)
        });
    }
}
