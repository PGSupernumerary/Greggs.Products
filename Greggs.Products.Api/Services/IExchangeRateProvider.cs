namespace Greggs.Products.Api.Services;

public interface IExchangeRateProvider
{
    decimal GetGbpToEurRate();
}
