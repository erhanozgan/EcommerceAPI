using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repo nesneleri
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Basket> BasketRepository { get; }
        public IGenericRepository<User> UserRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            // Repository'lerin başlatılması
            ProductRepository = new GenericRepository<Product>(_context);
            CategoryRepository = new GenericRepository<Category>(_context);
            BasketRepository = new GenericRepository<Basket>(_context);
            UserRepository = new GenericRepository<User>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }

    
