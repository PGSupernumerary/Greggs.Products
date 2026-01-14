using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductControllerTests
{
    [Fact]
    public void Get_ReturnsProductsFromDataAccess()
    {
        var expected = new List<Product>
        {
            new() { Name = "Test Product", PriceInPounds = 1.25m }
        };
        var dataAccess = new FakeProductAccess(expected);
        var controller = new ProductController(
            NullLogger<ProductController>.Instance,
            dataAccess,
            new ProductResponseMapper(new FixedRateProvider(1.11m)));

        var result = controller.Get(0, 5).ToList();

        Assert.Single(result);
        Assert.Equal("Test Product", result[0].Name);
        Assert.Equal(1.25m, result[0].PriceInPounds);
        Assert.Equal(1.39m, result[0].PriceInEuros);
    }

    [Fact]
    public void Get_PassesPagingToDataAccess()
    {
        var dataAccess = new FakeProductAccess(Array.Empty<Product>());
        var controller = new ProductController(
            NullLogger<ProductController>.Instance,
            dataAccess,
            new ProductResponseMapper(new FixedRateProvider(1.11m)));

        // Discard the result of this for the analyser. We do not need
        // the output, but we want to force enumeration.
        _ = controller.Get(2, 3).ToList();

        Assert.Equal(2, dataAccess.PageStart);
        Assert.Equal(3, dataAccess.PageSize);
    }

    private sealed class FakeProductAccess : IDataAccess<Product>
    {
        public int? PageStart { get; private set; }
        public int? PageSize { get; private set; }
        private readonly IEnumerable<Product> _result;

        public FakeProductAccess(IEnumerable<Product> result)
        {
            _result = result;
        }

        public IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            PageStart = pageStart;
            PageSize = pageSize;
            return _result;
        }
    }

    private sealed class FixedRateProvider : IExchangeRateProvider
    {
        private readonly decimal _rate;

        public FixedRateProvider(decimal rate)
        {
            _rate = rate;
        }

        public decimal GetGbpToEurRate()
        {
            return _rate;
        }
    }
}
