using System.Collections.Generic;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services;

public interface IProductResponseMapper
{
    IEnumerable<ProductResponseDto> Map(IEnumerable<Product> products, Currency currency);
}
