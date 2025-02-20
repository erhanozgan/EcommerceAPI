using System.Security.Claims;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BasketController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BasketController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Basket>>> GetBasketItems()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var basketItems = await _unitOfWork.BasketRepository.GetAllAsync(); // Filter by userId
        return Ok(basketItems);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Basket>> AddToBasket([FromBody] Basket basket)
    {
        await _unitOfWork.BasketRepository.AddAsync(basket);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetBasketItems), new { id = basket.Id }, basket);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> RemoveFromBasket(int id)
    {
        await _unitOfWork.BasketRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}