using ApiInescafe.DTOs.Product;
using ApiInescafe.Models;
using ApiInescafe.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace  ApiInescafe.Controller;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ResponseModel<List<ProductModel>>>> GetAllProducts()
    {
        var result = await _productService.GetAllProductsAsync();
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<ResponseModel<ProductModel>>> GetProductById(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("GetProductBySubstring")]
    public async Task<ActionResult<ResponseModel<ProductModel>>> GetProductBySubstring(string substring)
    {
        var result = await _productService.GetProductBySubstringAsync(substring);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("CreateProduct")]
    public async Task<ActionResult<ResponseModel<List<ProductModel>>>> CreateProduct([FromBody] ProductCreateDto product)
    {
        var result = await _productService.CreateProductAsync(product);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPatch("UpdateProduct")]
    public async Task<ActionResult<ResponseModel<List<ProductModel>>>> UpdateProduct([FromBody] ProductEditDto product)
    {
        var result = await _productService.UpdateProductAsync(product);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<ActionResult<ResponseModel<List<ProductModel>>>> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
