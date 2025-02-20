using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous] 
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        if (_unitOfWork == null)
        {
            return BadRequest("UnitOfWork is null.");
        }

        if (_unitOfWork.ProductRepository == null)
        {
            return BadRequest("ProductRepository is null.");
        }
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        return Ok(products);
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _unitOfWork.ProductRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}