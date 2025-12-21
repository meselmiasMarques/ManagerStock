using ManagerStock.Data;
using ManagerStock.Models;
using ManagerStock.Services;
using ManagerStock.ViewModels.Product;
using ManagerStock.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerStock.Controllers;

[ApiController]
public class ProductController(ProductService productService) : ControllerBase
{
    [HttpPost("v1/products")]
    public async Task<IActionResult> PostAsync(
        EditorProductViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<dynamic>(ModelState));

        try
        {
            var result = await productService.CreateAsync(model);
            return Created($"v1/products/{result.Id}", new ResultViewModel<Product>(result));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpGet("v1/products")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? search)
    {
        try
        {
            var products = await productService.GetAllAsync(minPrice, maxPrice, search);
            
            return Ok(new ResultViewModel<List<Product>>( products));
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpGet("v1/products/out-stock")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context
    )
    {
        try
        {
            var result = await productService.GetAllProductWhenStockAsync();
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpGet("v1/products/{id:int}")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context,
        [FromRoute] int id)
    {
        try
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpPut("v1/products/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorProductViewModel model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<dynamic>($"{ModelState.Values.SelectMany(v => v.Errors)}"));

        if (model.Price <= 0)
            return BadRequest("Informe um preço valido");

        if (model.StockAmout < 0)
            return BadRequest("Informe um stock valido");

        try
        {
          var result =  await productService.UpdateAsync(id, model);
            
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpPut("v1/products/baixar-estoque/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorStockViewModel model,
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<dynamic>($"{ModelState.Values.SelectMany(v => v.Errors)}"));

        if (model.StockAmout < 0)
            return BadRequest("Informe um stock valido");

        try
        {
          var product = productService.StockDown(id, model).Result.Data;

            if (product == null)
                return NotFound();
            
            return Ok(new ResultViewModel<dynamic>(product));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }

    [HttpDelete("v1/products/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
    {
        try
        {
            await productService.RemoveAsync(id);
            return NoContent();
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<dynamic>("Erro interno no servidor"));
        }
    }
}