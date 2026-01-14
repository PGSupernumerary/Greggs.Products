namespace Greggs.Products.Api.Services;

public class FixedExchangeRateProvider : IExchangeRateProvider
{
    private const decimal GbpToEurRate = 1.11m;

    public decimal GetGbpToEurRate()
    {
        return GbpToEurRate;
    }
}
