using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers;

/// <summary>
/// Sample controller for testing the DeconstructionService
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    /// <summary>
    /// Gets all products
    /// </summary>
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(new[] { "Product1", "Product2" });
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        return Ok($"Product {id}");
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="name">Product name</param>
    [HttpPost]
    public IActionResult CreateProduct([FromBody] string name)
    {
        return Created($"/api/products/1", name);
    }
}
