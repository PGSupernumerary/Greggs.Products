namespace Greggs.Products.Api.Models;

public class ProductResponseDto
{
    public string Name { get; set; }
    public Currency Currency { get; set; }
    public decimal? PriceInPounds { get; set; }
    public decimal? PriceInEuros { get; set; }
}
