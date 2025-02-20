using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Basket> BasketRepository { get; }
    IGenericRepository<User> UserRepository { get; }

    Task SaveAsync();
}