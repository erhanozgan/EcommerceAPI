using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
        return Ok(categories);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
    {
        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.CategoryRepository.UpdateAsync(category);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _unitOfWork.CategoryRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}