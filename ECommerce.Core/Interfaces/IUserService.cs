namespace ECommerce.Core.Interfaces;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
}