using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<ProductResponseDto>> Get(int pageStart = 0, int pageSize = 5)
    {
        if (pageStart < 0 || pageSize < 1)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid paging parameters.",
                Detail = "pageStart must be >= 0 and pageSize must be >= 1.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var products = _productData.List(pageStart, pageSize);
        return Ok(_productResponseMapper.Map(products));
    }
}
