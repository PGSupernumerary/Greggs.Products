using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _productData;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> productData)
    {
        _logger = logger;
        _productData = productData;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        if (pageStart < 0)
        {
            pageStart = 0;
        }

        if (pageSize < 1)
        {
            pageSize = 1;
        }
        
        return _productData.List(pageStart, pageSize);
    }
}