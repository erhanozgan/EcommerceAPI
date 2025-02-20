using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // 'Username' yerine 'UserName' kullanalım
    public User FindByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == username);
    }
}