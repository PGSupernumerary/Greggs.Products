using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _productData;
    private readonly IProductResponseMapper _productResponseMapper;

    public ProductController(
        ILogger<ProductController> logger,
        IDataAccess<Product> productData,
        IProductResponseMapper productResponseMapper)
    {
        _logger = logger;
        _productData = productData;
        _productResponseMapper = productResponseMapper;
    }

    [HttpGet]
    public IEnumerable<ProductResponseDto> Get(int pageStart = 0, int pageSize = 5)
    {
        if (pageStart < 0)
        {
            pageStart = 0;
        }

        if (pageSize < 1)
        {
            pageSize = 1;
        }

        var products = _productData.List(pageStart, pageSize);
        return _productResponseMapper.Map(products);
    }
}
