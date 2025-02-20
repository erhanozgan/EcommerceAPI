namespace ECommerce.Core.Interfaces;

public interface IUserRepository
{
    User FindByUsername(string username);
}