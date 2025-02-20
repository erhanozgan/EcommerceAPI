using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Password { get; set; } 
    public List<Basket> Baskets { get; set; }
}